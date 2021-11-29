using Microsoft.AspNetCore.Mvc;
using Polar.Controllers;
using Xunit;
 
namespace UnitTestApp.Tests
{
    public class TestControllerTests
    {
        [Fact]
        public void HttpGetReturnString()
        {
            // Arrange
            TestController testController = new TestController();
 
            // Act
            string result = testController.Get();
 
            // Assert
            Assert.Equal("Hello Angular", result);
        }
 
    }
}