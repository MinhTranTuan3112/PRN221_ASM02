using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;

namespace SalesRazorPageApp.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member?> GetMemberDetailsById(int memberId);  
    }
}