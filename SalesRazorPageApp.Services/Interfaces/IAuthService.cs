using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;

namespace SalesRazorPageApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Member?> Login(string email, string password);
    }
}