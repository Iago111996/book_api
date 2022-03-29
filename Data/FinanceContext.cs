using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data
{
    public class FinanceContext : IdentityDbContext<IdentityUser>
    {
        public FinanceContext(DbContextOptions<FinanceContext> opt) : base(opt) { }

        public DbSet<FinanceItem> FinanceItems { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}