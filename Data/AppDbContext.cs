using Microsoft.EntityFrameworkCore;
using PulsakuService.Models;

namespace PulsakuService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TopupTransaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TopupTransaction>()
                .HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<TopupTransaction>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<TopupTransaction>()
                .HasOne(x => x.Payment)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Payment>(x => x.TransactionId);
        }
    }
}