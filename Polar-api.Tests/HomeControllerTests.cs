using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Polar.Controllers;
using Polar.Models;
using Xunit;
using Moq;
 
namespace UnitTestApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void ExpectToGetRating()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PolarContext>()
            .UseInMemoryDatabase(databaseName: "PolarDatabase").Options;
            
            var context = new PolarContext(options);
            context.Users.Add(new User { UserName = "First", Score = 1 });
            context.Users.Add(new User { UserName = "Second", Score = 2 });
            context.Users.Add(new User { UserName = "Third", Score = 3 });
            context.Users.Add(new User { UserName = "Fourth", Score = 4 });
            context.SaveChanges();

            var mock = new Mock<IOptions<ApplicationSettings>>();
            HomeController controller = new HomeController(mock.Object, context);
 
            // Act
            var actionResult = await controller.Rating();
            var okResult = actionResult as OkObjectResult;
            var data = okResult.Value as RatingModel;
            var actual = data.Rating;
 
            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.True(IsEqualArrays(expected: GetTestUsers(), actual: actual));
        }

        [Fact]
        public async void ExpectToGetNotFoundDownload()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<PolarContext>().Options;
            var contextMock = new Mock<PolarContext>(dbOptions);

            var pathsSettings = new PathsSettings() { FilesPath = "./WrongDir/" };
            var appSettings = new ApplicationSettings() { Paths = pathsSettings };
            var options = Options.Create(appSettings);

            HomeController controller = new HomeController(options, contextMock.Object);

            // Act
            var actionResult = await controller.Download();
            
            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
            
        } 



        private List<RatingUserModel> GetTestUsers()
        {
            var users = new List<RatingUserModel>()
            {
                new RatingUserModel { Username = "First", Score = 1 },
                new RatingUserModel { Username = "Second", Score = 2 },
                new RatingUserModel { Username = "Third", Score = 3 },
                new RatingUserModel { Username = "Fourth", Score = 4 }
            };
            return users.OrderByDescending(x => x.Score).ToList<RatingUserModel>();
        }

        private bool IsEqualArrays(List<RatingUserModel> expected, List<RatingUserModel> actual)
        {
            bool isEqual = true;
            for(var i = 0; i < expected.Count; i++)
            {
                var a = expected[i];
                var b = actual[i];
                if (a.Username != b.Username || a.Score != b.Score)
                {
                    isEqual = false;
                    break;
                }
            }
            return isEqual;
        }
 
    }
}