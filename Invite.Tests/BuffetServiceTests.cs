using AutoMapper;
using Invite.Business.Interfaces.v1;
using Invite.Commons.LoggedUsers;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Invite.Services.v1;
using Moq;

namespace Invite.Tests;

public class BuffetServiceTests
{
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<ILoggedUser> _loggedUserMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IBuffetBusiness> _buffetBusinessMock;
    private readonly Mock<IInvoiceService> _invoiceServiceMock;
    private readonly Mock<IBuffetRepository> _buffetRepositoryMock;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly BuffetService _buffetService;

    public BuffetServiceTests()
    {
        _notificationContextMock = new Mock<INotificationContext>();
        _loggedUserMock = new Mock<ILoggedUser>();
        _mapperMock = new Mock<IMapper>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _buffetBusinessMock = new Mock<IBuffetBusiness>();
        _invoiceServiceMock = new Mock<IInvoiceService>();
        _buffetRepositoryMock = new Mock<IBuffetRepository>();
        _commentRepositoryMock = new Mock<ICommentRepository>();

        _buffetService = new BuffetService(
            _notificationContextMock.Object,
            _loggedUserMock.Object,
            _mapperMock.Object,
            _unitOfWorkMock.Object,
            _buffetBusinessMock.Object,
            _invoiceServiceMock.Object,
            _buffetRepositoryMock.Object,
            _commentRepositoryMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenNoNotificationsExist()
    {
        var userId = Guid.Empty;
        _loggedUserMock.Setup(x => x.GetId()).Returns(userId);

        var request = new BuffetCreateRequest
        {
            Name = "Buffet A",
            CNPJ = "12345678000199",
            PhoneNumber = "11987654321",
            City = "São Paulo",
            State = "SP",
            ServeInRadius = "30"
        };

        _buffetBusinessMock.Setup(x => x.ValidateForCreateAndUpdateAsync(request.Name, request.CNPJ, request.PhoneNumber))
            .ReturnsAsync(true);

        _buffetRepositoryMock.Setup(x => x.AddAsync(It.IsAny<BuffetModel>())).Returns((Task<bool>)Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CommitAsync(true));
        _notificationContextMock.Setup(x => x.HasNotifications).Returns(false);

        var result = await _buffetService.CreateAsync(request);

        Assert.True(result);
        _buffetRepositoryMock.Verify(x => x.AddAsync(It.Is<BuffetModel>(b => b.UserId == userId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(It.IsAny<bool>()), Times.Exactly(2));
    }
}
