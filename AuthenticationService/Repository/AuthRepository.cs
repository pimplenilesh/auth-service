using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;


namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private IAuthenticationContext context;

        public AuthRepository(IAuthenticationContext context)
        {
            this.context = context;
        }

        public User FindUserById(string userId)
        {
            var user = context.Users.FirstOrDefault(us => us.UserId == userId);
            return user;
        }

        public User LoginUser(string userId, string password)
        {
            var user = context.Users.FirstOrDefault(us => us.UserId == userId && us.Password == password);
            return user;
        }

        public bool RegisterUser(User user)
        {
            context.Users.Add(user);
            var saveResult = context.SaveChanges();
            return saveResult > 0;
        }
    }
}
