using Invite.Entities.Requests;
using Invite.Entities.Responses;
using Invite.Services.Interfaces.v1;
using Moq;

namespace Invite.Tests
{
    public class LoginServiceTests
    {
        private readonly Mock<ILoginService> _loginServiceMock;

        public LoginServiceTests()
        {
            _loginServiceMock = new Mock<ILoginService>();
        }

        [Fact]
        public async void LoginSuccess()
        {
            var request = new LoginRequest
            {
                Email = "milanezijoao51@gmail.com",
                Password = "Cba123#"
            };

            var expectedResponse = new LoginResponse
            {
                Token = "fake-jwt-token"
            };

            _loginServiceMock
                .Setup(service => service.Login(It.IsAny<LoginRequest>()))
                .ReturnsAsync(expectedResponse);
            var result = await _loginServiceMock.Object.Login(request);

            Assert.Equal(expectedResponse.Token, result.Token);
        }

        [Fact]
        public async Task LoginNotFound()
        {
            var request = new LoginRequest
            {
                Email = "notfound@example.com",
                Password = "wrongpassword"
            };

            _loginServiceMock
                .Setup(service => service.Login(It.IsAny<LoginRequest>()))
                .ReturnsAsync((LoginResponse)default!);

            var result = await _loginServiceMock.Object.Login(request);

            Assert.Null(result);
        }
    }
}