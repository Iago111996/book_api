using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Data;
using BookApi.Interfaces;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Services.CategoriesServeices
{
    public class CategoryServece : ICategoryService
    {
        private FinanceContext _context;

        public CategoryServece(FinanceContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToArrayAsync();

            }
            catch
            {
                throw;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                return category;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            try
            {
                IEnumerable<Category> categories;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    categories = await _context.Categories.Where(n => n.CAT_VNAME.Contains(name)).ToListAsync();
                }
                else
                {
                    categories = await GetCategories();
                }

                return categories;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(category).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


    }
}