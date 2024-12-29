using Ancient_Coin_Generater.Models_for_DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ancient_Coin_Generater.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<ImageRecord> ImageRecords { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Persist Security Info=False;Server=TWS3650.database.windows.net;Initial Catalog=AppDesign;User ID=ClassSharedUser;Password=AppDesign2024_Pa");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ImageRecord>(entity =>

                entity.ToTable("ImageRecord")
            );

            modelBuilder.Entity<ImageRecord>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            });
        }


    }
}
