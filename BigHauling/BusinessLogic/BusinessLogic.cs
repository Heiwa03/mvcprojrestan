using BigHauling.BusinessLogic.Core;
using BigHauling.BusinessLogic.Data;
using BigHauling.BusinessLogic.Interfaces;
using BigHauling.Domain.Entities;
using BigHauling.Helpers;
using Microsoft.AspNetCore.Http;
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

        private IServiceProvider GetRequestServices()
        {
            if (ServiceActivator.ServiceProvider == null)
            {
                throw new InvalidOperationException("ServiceActivator is not initialized.");
            }
            
            var httpContextAccessor = ServiceActivator.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available. This service can only be used within an active HTTP request.");
            }
            return httpContextAccessor.HttpContext.RequestServices;
        }

        public UserManager<ApplicationUser> GetUserManager()
        {
            return GetRequestServices().GetRequiredService<UserManager<ApplicationUser>>();
        }

        public SignInManager<ApplicationUser> GetSignInManager()
        {
             return GetRequestServices().GetRequiredService<SignInManager<ApplicationUser>>();
        }
    }
}