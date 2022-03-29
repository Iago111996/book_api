using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;

namespace BookApi.Services.FinanceItems
{
    public interface IFinanceItemService
    {
        Task CreateFinanceItem(FinanceItem item);
         Task<IEnumerable<FinanceItem>> GetFinanceItems();
        Task<FinanceItem> GetFinanceItemById(int id);
        Task<IEnumerable<FinanceItem>> GetFinanceItemByTitle(string name);
        Task UpdateFinanceItem(FinanceItem item);
        Task DeleteFinanceItem(int id);
    }
}