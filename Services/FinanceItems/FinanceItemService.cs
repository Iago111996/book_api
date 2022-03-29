using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Data;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Services.FinanceItems
{
    public class FinanceItemService : IFinanceItemService
    {
        private FinanceContext _context;

        public FinanceItemService(FinanceContext context)
        {
            _context = context;
        }

        public async Task CreateFinanceItem(FinanceItem financeDto)
        {
            try
            {
                _context.FinanceItems.Add(financeDto);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<FinanceItem>> GetFinanceItems()
        {
            try
            {
                return await _context.FinanceItems.ToArrayAsync();

            }
            catch
            {
                throw;
            }
        }

        public async Task<FinanceItem> GetFinanceItemById(int id)
        {
            try
            {
                var financeDto = await _context.FinanceItems.FindAsync(id);

                return financeDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<FinanceItem>> GetFinanceItemByTitle(string name)
        {
            try
            {
                IEnumerable<FinanceItem> financeDto;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    financeDto = await _context.FinanceItems.Where(n => n.FIN_VTITLE.Contains(name)).ToListAsync();
                }
                else
                {
                    financeDto = await GetFinanceItems();
                }

                return financeDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateFinanceItem(FinanceItem financeDto)
        {
            try
            {
                _context.Entry(financeDto).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteFinanceItem(int id)
        {
            var financeDto = await _context.FinanceItems.FindAsync(id);

            _context.FinanceItems.Remove(financeDto).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

    }
}