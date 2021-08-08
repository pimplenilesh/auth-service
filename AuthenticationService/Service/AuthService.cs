using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repository;
        
        public AuthService(IAuthRepository _repository)
        {
            this.repository = _repository;
        }

        public bool IsUserExists(string userId)
        {
            var result = repository.FindUserById(userId);
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User LoginUser(string userId, string password)
        {
            var result = repository.LoginUser(userId, password);
            if(result == null)
            {
                throw new Exceptions.UserNotFoundException($"User with this id {userId} and password {password} does not exist");
            }

            return result;
        }

        public bool RegisterUser(User user)
        {
            user.AddedDate = DateTime.Now;
            var result = repository.RegisterUser(user);
            if(result == false)
            {
                throw new Exceptions.UserNotCreatedException($"User with this id {user.UserId} already exists");
            }

            return result;
        }
    }
}
