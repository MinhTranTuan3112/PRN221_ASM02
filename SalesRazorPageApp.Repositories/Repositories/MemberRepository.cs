using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}