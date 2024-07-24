using CARDGENERATOR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CARDGENERATOR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Card> Cards { get; set; }
    }

}
