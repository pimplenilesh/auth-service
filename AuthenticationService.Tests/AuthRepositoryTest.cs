using System;
using AuthenticationService.Models;
using AuthenticationService.Repository;
using Xunit;

namespace AuthenticationService.Test
{
    public class AuthRepositoryTest:IClassFixture<DatabaseFixture>
    {
        public readonly IAuthRepository repository;

        public AuthRepositoryTest(DatabaseFixture _fixture)
        {
            repository = new AuthRepository(_fixture.context);
        }

        [Fact]
        public void RegisterUserShouldReturnTrue()
        {
            User user = new User {UserId="nilesh", Password="admin123", FirstName="nilesh", LastName="pimple", Role="admin", AddedDate=new DateTime() };

            var actual= repository.RegisterUser(user);

            Assert.True(actual);
            var checkuser = repository.FindUserById("nilesh");
            Assert.Equal<User>(user, checkuser);
        }

        [Fact]
        public void LoginUserShouldReturnTrue()
        {
            string userId = "nilesh";
            string password = "admin123";

            var actual = repository.LoginUser(userId,password);

            Assert.IsAssignableFrom<User>(actual);
        }
    }
}
