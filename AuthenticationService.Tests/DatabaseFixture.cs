using System;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Test
{
    public class DatabaseFixture : IDisposable
    {
        public IAuthenticationContext context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseInMemoryDatabase(databaseName: "AuthDb")
                .Options;

            //Initializing DbContext with InMemory
            context = new AuthenticationContext(options);

            // Insert seed data into the database using one instance of the context
            context.Users.Add(new User { UserId="admin",Password="admin123", FirstName="Mukesh", LastName="Goel", Role="admin", AddedDate=new DateTime()  });
            context.SaveChanges();
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
