using System;
using Xunit;
using Moq;
using AuthenticationService.Service;
using AuthenticationService.Models;
using AuthenticationService.Controllers;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Exceptions;

namespace AuthenticationService.Test
{
    public class AuthControllerTest
    {
        [Fact]
        public void RegisterShouldReturnCreatedResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();

            User user = new User { UserId = "Nilesh", Password = "admin123", FirstName = "Nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };


            mockService.Setup(service => service.RegisterUser(user)).Returns(true);
            var controller = new AuthController(mockService.Object, mockTokenGenerator.Object, null);

            var actual = controller.Register(user);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            Assert.True((bool) actionReult.Value);
        }

        [Fact]
        public void LoginShouldReturnOkResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();

            var loginuser = new User { UserId = "Nilesh", Password = "admin123" };
            User user = new User { UserId = "Nilesh", Password = "admin123", FirstName = "Nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };

            mockService.Setup(service => service.LoginUser(loginuser.UserId,loginuser.Password)).Returns(user);
            var controller = new AuthController(mockService.Object, mockTokenGenerator.Object, null);

            var actual = controller.Login(loginuser);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            //Nilesh: Commented it as after JWT login should return token and not an user
            //Assert.IsAssignableFrom<User>(actionReult.Value);
        }



        [Fact]
        public void RegisterShouldReturnConflictResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();

            User user = new User { UserId = "Nilesh", Password = "admin123", FirstName = "Nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };


            mockService.Setup(service => service.RegisterUser(user)).Throws(new UserNotCreatedException(""));
            var controller = new AuthController(mockService.Object, mockTokenGenerator.Object, null);

            var actual = controller.Register(user);

            var actionResult = Assert.IsType<ConflictResult>(actual);
        }

        [Fact]
        public void LoginShouldReturnNotFoundResult()
        {
            var mockService = new Mock<IAuthService>();
            var mockTokenGenerator = new Mock<ITokenGenerator>();

            var loginuser = new User { UserId = "Nilesh", Password = "admin123" };
            User user = new User { UserId = "Nilesh", Password = "admin123", FirstName = "Nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };

            mockService.Setup(service => service.LoginUser(loginuser.UserId, loginuser.Password)).Throws(new UserNotFoundException(""));
            var controller = new AuthController(mockService.Object, mockTokenGenerator.Object, null);

            var actual = controller.Login(loginuser);

            var actionReult = Assert.IsType<NotFoundResult>(actual);
        }
    }
}
