using CCValidation.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CCValidation.Data
{
    public class CardDBContext : DbContext
    {
        public CardDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Card> Cards { get; set; }
    }
}
