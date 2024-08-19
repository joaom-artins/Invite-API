using System.Net.Http.Json;
using AutoMapper;
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
    IMapper _mapper,
    INotificationContext _notificationContext,
    IPlanRepository _planRepository,
    IHallRepository _hallRepository,
    IBuffetRepository _buffetRepository,
    IEventRepository _eventRepository,
    IUserRepository _userRepository,
    IInvoiceItemizedRepository _invoiceItemizedRepository,
    IInvoiceRepository _invoiceRepository,
    ICerimonialistRepository _cerimonialistRepository
) : IInvoiceService
{
    public async Task<IEnumerable<InvoiceResponse>> FindByUserAsync()
    {
        var records = await _invoiceRepository.FindByUserAsync(_loggedUser.GetId());

        return _mapper.Map<IEnumerable<InvoiceResponse>>(records);
    }

    public async Task<InvoiceResponse> GetByReferenceAsync(string reference)
    {
        var record = await _invoiceRepository.GetByUserAndReferenceAsync(_loggedUser.GetId(), reference);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invoice.NotFound
            );
            return default!;
        }

        return _mapper.Map<InvoiceResponse>(record);
    }

    public async Task<bool> CreateAsync(Guid userId, bool isAutomated,
        EventModel? eventModel = null,
        BuffetModel? buffet = null,
        HallModel? hall = null,
        CerimonialistModel? cerimonialist = null)
    {
        string reference;
        bool exists;
        do
        {
            reference = References.Generate();
            exists = await _invoiceRepository.ExistsByReference(reference);
        } while (exists);

        var invoice = new InvoiceModel
        {
            UserId = userId,
            Reference = reference,
            Status = InvoiceStatusEnum.Unpaid
        };
        await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.CommitAsync();

        if (isAutomated)
        {
            var halls = await _hallRepository.FindByUserAsync(userId);
            if (halls.Any())
            {
                foreach (var hallUser in halls)
                {
                    var invoiceItemized = new InvoiceItemizedModel
                    {
                        InvoiceId = invoice.Id,
                        Price = _appSettings.Tax.Hall,
                        StarDate = DateOnly.FromDateTime(DateTime.Now),
                        FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_appSettings.Invoice.DaysBeforeCreate)),
                        Title = hallUser.Name,
                        Description = hallUser.Name,
                        HallId = hallUser.Id
                    };
                    await _invoiceItemizedRepository.AddAsync(invoiceItemized);
                    await _unitOfWork.CommitAsync();
                }
            }

            var buffets = await _buffetRepository.FindByUserAsync(userId);
            if (buffets.Any())
            {
                foreach (var buffetUser in buffets)
                {
                    var invoiceItemized = new InvoiceItemizedModel
                    {
                        InvoiceId = invoice.Id,
                        Price = _appSettings.Tax.Buffet,
                        StarDate = DateOnly.FromDateTime(DateTime.Now),
                        FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_appSettings.Invoice.DaysBeforeCreate)),
                        Title = buffetUser.Name,
                        Description = buffetUser.Name,
                        BuffetId = buffetUser.Id
                    };
                    await _invoiceItemizedRepository.AddAsync(invoiceItemized);
                    await _unitOfWork.CommitAsync();
                }
            }

            var cerimonialists = await _cerimonialistRepository.FindByUserAsync(userId);
            if (cerimonialists.Any())
            {
                foreach (var cerimonialistUser in buffets)
                {
                    var invoiceItemized = new InvoiceItemizedModel
                    {
                        InvoiceId = invoice.Id,
                        Price = _appSettings.Tax.Cerimonialist,
                        StarDate = DateOnly.FromDateTime(DateTime.Now),
                        FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_appSettings.Invoice.DaysBeforeCreate)),
                        Title = cerimonialistUser.Name,
                        Description = cerimonialistUser.Name,
                        BuffetId = cerimonialistUser.Id
                    };
                    await _invoiceItemizedRepository.AddAsync(invoiceItemized);
                    await _unitOfWork.CommitAsync();
                }
            }

            var value = (halls.Count() * _appSettings.Tax.Hall) + (buffets.Count() * _appSettings.Tax.Buffet) + (cerimonialists.Count() * _appSettings.Tax.Cerimonialist);

            invoice.Price = value;
            invoice.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_appSettings.Invoice.DaysBeforeCreate));
            invoice.Discount = 0;
            invoice.Total = value - 0;
        }

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

        if (cerimonialist is not null)
        {
            var invoiceItemized = new InvoiceItemizedModel
            {
                InvoiceId = invoice.Id,
                Price = _appSettings.Tax.Cerimonialist,
                StarDate = DateOnly.FromDateTime(DateTime.Now),
                FinishDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Title = cerimonialist.Name,
                Description = cerimonialist.Name,
                CerimonialisId = cerimonialist.Id
            };
            await _invoiceItemizedRepository.AddAsync(invoiceItemized);
            await _unitOfWork.CommitAsync();

            invoice.DueDate = invoiceItemized.FinishDate;
            invoice.Price = invoiceItemized.Price;
            invoice.Discount = 0;
            invoice.Total = invoiceItemized.Price - 0;
        }

        var externalId = await CreateInExternalServiceAsync(userId, invoice);
        invoice.ExternalId = externalId;
        _invoiceRepository.Update(invoice);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> PayAsync(Guid id, InvoicePayRequest request)
    {
        var invoice = await _invoiceRepository.GetByIdWithUserAsync(id);
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

        if (invoice.User.DueDay is null)
        {
            invoice.User.DueDay = DateTime.Now.Day;
            _userRepository.Update(invoice.User);
            await _unitOfWork.CommitAsync();
        }

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

    private async Task<string> CreateInExternalServiceAsync(Guid userId, InvoiceModel invoice)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

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
