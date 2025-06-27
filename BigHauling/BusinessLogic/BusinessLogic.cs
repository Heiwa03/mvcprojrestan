using BigHauling.BusinessLogic.Core;
using BigHauling.BusinessLogic.Data;
using BigHauling.BusinessLogic.Interfaces;
using BigHauling.Domain.Entities;
using BigHauling.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BigHauling.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly ApplicationDbContext _context;

        public BusinessLogic()
        {
            // Manually build configuration to get the connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        public IAdminAPI GetAdminAPI()
        {
            return new AdminAPI(_context);
        }

        public ITruckService GetTruckService()
        {
            return new TruckService(_context);
        }

        public UserManager<ApplicationUser> GetUserManager()
        {
            if (ServiceActivator.ServiceProvider == null)
            {
                throw new InvalidOperationException("ServiceActivator is not initialized.");
            }
            return ServiceActivator.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        public SignInManager<ApplicationUser> GetSignInManager()
        {
             if (ServiceActivator.ServiceProvider == null)
            {
                throw new InvalidOperationException("ServiceActivator is not initialized.");
            }
            return ServiceActivator.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }
    }
} 