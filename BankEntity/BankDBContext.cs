using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankRepository
{
    public partial class BankDBContext : DbContext
    {
        public BankDBContext()
        {
        }

        public BankDBContext(DbContextOptions<BankDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("BankDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.RowId).ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnType("numeric(18, 4)")
                    .HasColumnName("amount");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .HasColumnName("description")
                    .IsFixedLength(true);

                entity.Property(e => e.ExternalId).HasColumnName("externalId");

                entity.Property(e => e.FromAccount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fromAccount");

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.ToAccount)
                    .HasMaxLength(50)
                    .HasColumnName("toAccount");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK_Transactions_ToCustomers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
