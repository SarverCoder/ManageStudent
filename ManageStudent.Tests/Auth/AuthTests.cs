using AutoFixture;
using FluentAssertions;
using ManageStudent.Options;
using ManageStudent.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;

namespace ManageStudent.Tests.Auth
{
    public class AuthTests
    {
        private readonly AutoMocker _mocker = new();
        private readonly Fixture _fixture = new();


        public AuthTests()
        {
            
        }



        [Fact]
        public async Task GenerateJwtToken_ValidInput_ShouldReturnValidToken()
        {
            // Arrange
            var userId = 1;
            var email = "exmaple@gmail.com";
            var role = "Admin";

            var tokenValid = _fixture.Create<string>();

            var jwtSettings = _fixture.Create<JwtSettings>();

            var mockOptions = new Mock<IOptions<JwtSettings>>();
            mockOptions.Setup(x => x.Value).Returns(jwtSettings);

            _mocker.GetMock<IAuthService>()
                .Setup(x => x.GenerateJwtToken(userId, email, role))
                .ReturnsAsync(tokenValid);

            var authService = _mocker.CreateInstance<AuthService>();
            // Act

            var token = await authService.GenerateJwtToken(userId, email, role);

            // Assert

            token.Should().NotBeNullOrEmpty();
            token.Should().Be(tokenValid);




        }
    }
}
