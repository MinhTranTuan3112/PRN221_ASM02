using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;

namespace SalesRazorPageApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}