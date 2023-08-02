
using UnitConverterAPI.Interface;
using Moq;
using UnitConverterAPI.Model.Response;
using UnitConverterAPI.Model.Request;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using UnitConverterAPITests1.Fixtures;
using Microsoft.Extensions.Options;
using UnitConverterAPI.Options;
using Microsoft.Extensions.Logging;
using UnitConverterAPI.Service;
using UnitConverterAPI.Context;
using Microsoft.EntityFrameworkCore;
using UnitConverterAPI.Model.DbContext;
using Moq.EntityFrameworkCore;

namespace UnitConverterAPI.Controllers.Tests
{
   // [TestClass()]
    public class AuthenticationControllerTests
    {
        // [Fact]
        //public async Task Token_Verify_Auth()
        //{
        //    //Arrange
        //    var model = new TokenRequest()
        //    {
        //        Password = "test",
        //        Username = "tes123t"
        //    };

        //    var mockAuthService = new Mock<IAuthorizationsService>();

        //    mockAuthService
        //        .Setup(service => service.Token(new TokenRequest()))
        //        .ReturnsAsync((new ResponseResult(), new TokenResponse()));

        //    var controller = new AuthenticationController(mockAuthService.Object);

        //    //Act
        //    var result = await controller.Token(model);

        //    //Assert
        //    mockAuthService.Verify(
        //        service => service.Token(model),
        //        Times.Once());
        // }
        //
        [Fact]
        public async Task MockDattabase1()
        {
            var options = new Mock<IOptions<TokenOptions>>();
            var logger = new Mock<ILogger<AuthorizationService>>();
            var context = new Mock<IAuthenticationDbContextService>();

            var employeeContextMock = new Mock<AuthenticationDbContext>();

            employeeContextMock.Setup<DbSet<UserModel>>(x => x.User).ReturnsDbSet(Fixtures.GetUsers());

            var service = new AuthorizationService(logger.Object, options.Object, employeeContextMock.Object);

            var model = new TokenRequest()
            {
                Password = "test",
                Username = "tes123t"
            };

            var k = service.Token(model);

        }




        //[Fact]
        //public async Task Get_OnSuccess__Token_Model1()
        //{
        //    var model = new TokenRequest()
        //    {
        //        Password = "test",
        //        Username = "tes123t"
        //    };

        //    //Arrange
        //    var mockAuthService = new Mock<IAuthorizationsService>();

        //    mockAuthService.
        //        Setup(x => x.Token(new TokenRequest()))
        //        .ReturnsAsync(Fixtures.Token);

        //    var controller = new AuthenticationController(mockAuthService.Object);

        //    //Act
        //    var result = (OkObjectResult) await controller.Token(model);

        //    //Assert
        //    result.Should().BeOfType<OkObjectResult>();
        //    var objectResult = (OkObjectResult)result;
        //    objectResult.Value.Should().BeOfType<TokenResponse>();
        //}

        [Fact]
        public async Task Get_OnSuccess__Token_Model()
        {
            //Arrange
            var mockAuthService = new Mock<IAuthorizationsService>();

            mockAuthService.
                Setup(x => x.test())
                .ReturnsAsync(new TokenResponse());

            var controller = new AuthenticationController(mockAuthService.Object);

            //Act
            var result = await controller.test();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<TokenResponse>();
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockAuthService = new Mock<IAuthorizationsService>();

            mockAuthService.
                Setup(x => x.test())
                .ReturnsAsync(new TokenResponse());

            var controller = new AuthenticationController(mockAuthService.Object);

            //Act
            var result = (OkObjectResult) await controller.test();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnTokenModel()
        {
            //Arrange
            var mockAuthService = new Mock<IAuthorizationsService>();

            mockAuthService.
                Setup(x => x.test())
                .ReturnsAsync(Fixtures.GetTokens); 

            var controller = new AuthenticationController(mockAuthService.Object);

            //Act
            var result = (OkObjectResult)await controller.test();

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}