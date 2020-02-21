using Microsoft.EntityFrameworkCore;
using NewQuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewQuotesApi.Data
{
    public class QuotesDbContext : DbContext
    {
        public QuotesDbContext(DbContextOptions<QuotesDbContext>options):base(options)
        {

        }
        public DbSet<Quote> Quotes { get; set; }
    }
}
