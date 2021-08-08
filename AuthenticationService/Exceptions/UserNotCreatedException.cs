using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Exceptions
{
    public class UserNotCreatedException : ApplicationException
    {
        public UserNotCreatedException(string message) : base(message)
        {
        }
    }
}
