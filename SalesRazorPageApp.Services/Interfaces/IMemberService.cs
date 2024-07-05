using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;

namespace SalesRazorPageApp.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetMembers();

        Task<Member> GetMemberDetailsById(int id);

        Task<int> CreateMember(Member member);

        Task UpdateMember(Member member);  

        Task DeleteMember(int memberId);

        Task<Member?> GetMemberById(int memberId);

    }
}