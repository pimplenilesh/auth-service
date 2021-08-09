using System;
using Xunit;
using Moq;
using AuthenticationService.Repository;
using AuthenticationService.Models;
using AuthenticationService.Service;
using AuthenticationService.Exceptions;

namespace AuthenticationService.Test
{
    public class AuthServiceTest
    {
        [Fact]
        public void RegisterUserShouldReturnTrue()
        {
            var mockRepo = new Mock<IAuthRepository>();
            User user = new User { UserId = "nilesh", Password = "admin123", FirstName = "nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };
            mockRepo.Setup(repo => repo.RegisterUser(user)).Returns(true);
            var service = new AuthService(mockRepo.Object);

            var actual = service.RegisterUser(user);

            Assert.True(actual);
        }

        [Fact]
        public void LoginUserShouldReturnUser()
        {
            var mockRepo = new Mock<IAuthRepository>();
            string userId = "nilesh";
            string password = "admin123";

            User user = new User { UserId = "nilesh", Password = "admin123", FirstName = "nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };
            mockRepo.Setup(repo => repo.LoginUser(userId,password)).Returns(user);
            var service = new AuthService(mockRepo.Object);

            var actual = service.LoginUser(userId,password);

            Assert.IsAssignableFrom<User>(actual);
        }


        [Fact]
        public void RegisterUserShouldThrowException()
        {
            var mockRepo = new Mock<IAuthRepository>();
            User user = new User { UserId = "nilesh", Password = "admin123", FirstName = "nilesh", LastName = "Pimple", Role = "admin", AddedDate = new DateTime() };
            mockRepo.Setup(repo => repo.RegisterUser(user)).Returns(false);
            var service = new AuthService(mockRepo.Object);

            var actual = Assert.Throws<UserNotCreatedException>(() => service.RegisterUser(user));
            Assert.Equal($"User with this id {user.UserId} already exists", actual.Message);
        }

        [Fact]
        public void LoginUserShouldThrowException()
        {
            var mockRepo = new Mock<IAuthRepository>();
            string userId = "nilesh";
            string password = "admin123";

            User user = null;
            mockRepo.Setup(repo => repo.LoginUser(userId, password)).Returns(user);
            var service = new AuthService(mockRepo.Object);

            var actual = Assert.Throws<UserNotFoundException>(() => service.LoginUser(userId,password));
            Assert.Equal($"User with this id {userId} and password {password} does not exist", actual.Message);
        }
    }
}
