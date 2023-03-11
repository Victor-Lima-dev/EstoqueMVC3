using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueMVC3.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueMVC3.context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        
        
    }
}