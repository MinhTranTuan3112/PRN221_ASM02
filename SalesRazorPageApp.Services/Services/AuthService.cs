using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;

namespace SalesRazorPageApp.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member?> Login(string email, string password)
        {
            var member = await _unitOfWork.MemberRepository.FindOneAsync(m => m.Email == email && m.Password == password);

            return member;
            
        }
    }
}