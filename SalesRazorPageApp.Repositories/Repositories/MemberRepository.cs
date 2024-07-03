using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;

namespace SalesRazorPageApp.Repositories.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly SalesManagementDbContext _context;
        
        public MemberRepository(SalesManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Member?> GetMemberDetailsById(int memberId)
        {
            return await _context.Members.Include(m => m.Orders)
                                    .SingleOrDefaultAsync(m => m.MemberId == memberId);
        }
    }
}