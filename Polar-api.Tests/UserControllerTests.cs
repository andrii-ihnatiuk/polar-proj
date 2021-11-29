using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Polar.Controllers;
using Polar.Models;
using Xunit;
using Moq;
 
namespace UnitTestApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async void ExpectToSendToken()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<PolarContext>().Options;
            var context = new Mock<PolarContext>(dbOptions);
            var appSettings = new ApplicationSettings() { AuthOptions = new AuthOptionsSettings() { Secret = "qwerty123456789qwerty"} };
            var options = Options.Create(appSettings);
            var store = new Mock<IUserStore<User>>();
            var userMngrMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userMngrMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new User() { Id = "1" });
            userMngrMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            var signInMngrMock = new Mock<SignInManager<User>>(
                userMngrMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null
            );
            UserController controller = new UserController(userMngrMock.Object, signInMngrMock.Object, options, context.Object);
 
            // Act
            var actionResult = await controller.Login(new UserLoginModel() { Login = "First", Password = "qwerty1" });
            var okResult = actionResult as OkObjectResult;
            var data = okResult.Value as LoginOkModel;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(data.Token);

        }

        [Fact]
        public async void ExpectToSendLoginErrorOnWrongPassword()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<PolarContext>().Options;
            var context = new Mock<PolarContext>(dbOptions);
            var appSettings = new ApplicationSettings();
            var options = Options.Create(appSettings);
            var store = new Mock<IUserStore<User>>();
            var userMngrMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userMngrMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new User() { Id = "1" });
            userMngrMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            var signInMngrMock = new Mock<SignInManager<User>>(
                userMngrMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null
            );
            UserController controller = new UserController(userMngrMock.Object, signInMngrMock.Object, options, context.Object);

            // Act
            var actionResult = await controller.Login(new UserLoginModel() { Login = "First", Password = "wrongPassword" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void ExpectToSendLoginErrorOnWrongLogin()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<PolarContext>().Options;
            var context = new Mock<PolarContext>(dbOptions);
            var appSettings = new ApplicationSettings();
            var options = Options.Create(appSettings);
            var store = new Mock<IUserStore<User>>();
            var userMngrMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userMngrMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            userMngrMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);
            var signInMngrMock = new Mock<SignInManager<User>>(
                userMngrMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null
            );
            UserController controller = new UserController(userMngrMock.Object, signInMngrMock.Object, options, context.Object);

            // Act
            var actionResult = await controller.Login(new UserLoginModel() { Login = "WrongLogin", Password = "password" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void ExpectToCreateNewUserOnRegistration()
        {
             // Arrange
            var dbOptions = new DbContextOptionsBuilder<PolarContext>().Options;
            var context = new Mock<PolarContext>(dbOptions);
            var appSettings = new ApplicationSettings();
            var options = Options.Create(appSettings);
            var store = new Mock<IUserStore<User>>();
            var userMngrMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userMngrMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var signInMngrMock = new Mock<SignInManager<User>>(
                userMngrMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null
            );
            UserController controller = new UserController(userMngrMock.Object, signInMngrMock.Object, options, context.Object);

            // Act
            var actionResult = await controller.Register(new UserRegisterModel() 
            { 
                Email = "email@test.dotnet", Password = "pass", UserName = "user" 
            });
            
            // Assert
            userMngrMock.Verify(m => m.CreateAsync(
                It.Is<User>(u => u.Email == "email@test.dotnet" && u.UserName == "user"), 
                It.Is<string>(s => s == "pass")), 
                Times.Once);
        }


    }
}