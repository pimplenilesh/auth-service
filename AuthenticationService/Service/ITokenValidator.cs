using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Service
{
    public interface ITokenValidator
    {
        bool ValidateToken(AuthToken token);
    }
}
