using System.Net.Http.Json;
using Invite.Commons;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Enums;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace Invite.Services.v1;

public class InvoiceService(
    AppSettings _appSettings,
    IUnitOfWork _unitOfWork,
    ILoggedUser _loggedUser,
    INotificationContext _notificationContext,
    IPlanRepository _planRepository,
    IHallRepository _hallRepository,
    IBuffetRepository _buffetRepository,
    IEventRepository _eventRepository,
    IUserRepository _userRepository,
    IInvoiceItemizedRepository _invoiceItemizedRepository,
    IInvoiceRepository _invoiceRepository
) : IInvoiceService
{
    public async Task<bool> CreateAsync(EventModel? eventModel = null, BuffetModel? buffet = null, HallModel? hall = null)
    {
        var user = await _userRepository.GetByIdAsync(_loggedUser.GetId());
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false!;
        }

        string reference;
        bool exists;
        do
        {
            reference = References.Generate();
            exists = await _invoiceRepository.ExistsByReference(reference);
        } while (exists);

        var invoice = new InvoiceModel
        {
            UserId = user.Id,
            Reference = reference,
            Status = InvoiceStatusEnum.Unpaid
        };
        await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.CommitAsync();

        if (eventModel is not null)
        {
            var plan = await _planRepository.GetByIdAsync(eventModel.PlanId);
            if (plan is null)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status404NotFound,
                    title: NotificationTitle.NotFound,
                    detail: NotificationMessage.Plan.NotFound
                );
                return false;
            }

            var invoiceItemized = new InvoiceItemizedModel
            {
                InvoiceId = invoice.Id,
                Price = plan.Price,
                StarDate = DateOnly.FromDateTime(DateTime.Now),
                FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Title = eventModel.Name,
                Description = eventModel.Name,
                EventId = eventModel.Id,
            };
            await _invoiceItemizedRepository.AddAsync(invoiceItemized);
            await _unitOfWork.CommitAsync();

            invoice.DueDate = invoiceItemized.FinishDate;
            invoice.Price = invoiceItemized.Price;
            invoice.Discount = 0;
            invoice.Total = invoiceItemized.Price - 0;
        }

        if (buffet is not null)
        {
            var invoiceItemized = new InvoiceItemizedModel
            {
                InvoiceId = invoice.Id,
                Price = _appSettings.Tax.Buffet,
                StarDate = DateOnly.FromDateTime(DateTime.Now),
                FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Title = buffet.Name,
                Description = buffet.Name,
                BuffetId = buffet.Id
            };
            await _invoiceItemizedRepository.AddAsync(invoiceItemized);
            await _unitOfWork.CommitAsync();

            invoice.DueDate = invoiceItemized.FinishDate;
            invoice.Price = invoiceItemized.Price;
            invoice.Discount = 0;
            invoice.Total = invoiceItemized.Price - 0;
        }

        if (hall is not null)
        {
            var invoiceItemized = new InvoiceItemizedModel
            {
                InvoiceId = invoice.Id,
                Price = _appSettings.Tax.Hall,
                StarDate = DateOnly.FromDateTime(DateTime.Now),
                FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Title = hall.Name,
                Description = hall.Name,
                HallId = hall.Id
            };
            await _invoiceItemizedRepository.AddAsync(invoiceItemized);
            await _unitOfWork.CommitAsync();

            invoice.DueDate = invoiceItemized.FinishDate;
            invoice.Price = invoiceItemized.Price;
            invoice.Discount = 0;
            invoice.Total = invoiceItemized.Price - 0;
        }

        var externalId = await CreateInExternalServiceAsync(user, invoice);
        invoice.ExternalId = externalId;
        _invoiceRepository.Update(invoice);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> PayAsync(Guid id, InvoicePayRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice is null)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status404NotFound,
               title: NotificationTitle.NotFound,
               detail: NotificationMessage.Invoice.NotFound
           );
            return false;
        }

        await PayWithCreditCardAsync(invoice.ExternalId!, request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        invoice.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        invoice.PaymentMethod = request.PaymentMethod;
        invoice.Status = InvoiceStatusEnum.Paid;
        _invoiceRepository.Update(invoice);
        await _unitOfWork.CommitAsync();

        var invoiceItemized = await _invoiceItemizedRepository.GetByInvoiceWithIncludesAsync(id);
        if (invoiceItemized is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.InvoiceItemized.NotFound
            );
            return false;
        }

        if (invoiceItemized.Hall is not null)
        {
            invoiceItemized.Hall.Paid = true;
            _hallRepository.Update(invoiceItemized.Hall);
            await _unitOfWork.CommitAsync();
        }

        if (invoiceItemized.Buffet is not null)
        {
            invoiceItemized.Buffet.Paid = true;
            _buffetRepository.Update(invoiceItemized.Buffet);
            await _unitOfWork.CommitAsync();
        }

        if (invoiceItemized.Event is not null)
        {
            invoiceItemized.Event.Paid = true;
            _eventRepository.Update(invoiceItemized.Event);
            await _unitOfWork.CommitAsync();
        }

        return true;
    }

    private async Task<string> CreateInExternalServiceAsync(UserModel user, InvoiceModel invoice)
    {
        var body = new
        {
            customer = user.ExternalId,
            value = invoice.Total,
            dueDate = invoice.DueDate,
            externalReference = invoice.Id,
            billingType = "Undefined"
        };

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("access_token", _appSettings.Asaas.ApiKey);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Invites");

        var result = await httpClient.PostAsJsonAsync($"{_appSettings.Asaas.ApiUrl}/payments", body);
        if (!result.IsSuccessStatusCode)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invoice.FailOnCreateInExternalService
            );
            return default!;
        }

        var response = await result.Content.ReadFromJsonAsync<InvoiceResponseFromAsaas>();

        return response!.Id;
    }

    private async Task<bool> PayWithCreditCardAsync(string externalId, InvoicePayRequest request)
    {
        var body = new
        {
            creditCard = new
            {
                holderName = request.CreditCard!.HolderName,
                number = request.CreditCard!.Number,
                expiryMonth = request.CreditCard!.ExpiryDate[..2],
                expiryYear = $"20{request.CreditCard!.ExpiryDate[3..5]}",
                ccv = request.CreditCard.CCV
            },
            creditCardHolderInfo = new
            {
                name = request.CreditCard!.HolderName,
                cpfCnpj = request.CreditCard.HolderCpfCnpj,
                email = request.CreditCard.HolderEmail,
                mobilePhone = request.CreditCard.HolderMobilePhone,
                postalCode = request.CreditCard.HolderPostalCode,
                addressNumber = request.CreditCard.HolderAddressNumber
            }
        };

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("access_token", _appSettings.Asaas.ApiKey);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Invites");

        var result = await httpClient.PostAsJsonAsync($"{_appSettings.Asaas.ApiUrl}/payments/{externalId}/payWithCreditCard", body);

        var response = await result.Content.ReadFromJsonAsync<InvoicePayWithCardResponse>();
        if (response!.Status != "CONFIRMED" && response!.Status != "RECEIVED")
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invoice.UnablePay
            );
            return false;
        }

        return true;
    }
}
