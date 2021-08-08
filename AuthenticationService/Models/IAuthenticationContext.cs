using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models
{
    public interface IAuthenticationContext
    {
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
