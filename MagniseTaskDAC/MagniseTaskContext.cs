using MagniseTaskDAC.Entity;
using Microsoft.EntityFrameworkCore;

namespace MagniseTaskDAC
{
    public class MagniseTaskContext : DbContext
    {
        public MagniseTaskContext(DbContextOptions<MagniseTaskContext> options) : base(options)
        {
        }

        public DbSet<Crypto> Crypto { get; set; }
        public DbSet<CryptoHistory> CryptoHistory { get; set; }
    }
}
