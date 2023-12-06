using BudgetApp.Areas.Identity.Data;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetApp.Areas.Identity.Data;

public class DBContext : IdentityDbContext<BudgetAppUser>
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public DbSet<BudgetAppUser> BudgetAppUsers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.ApplyConfiguration(new TransactionEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<BudgetAppUser>
{
    public void Configure(EntityTypeBuilder<BudgetAppUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.MiddleName).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.PhoneNum).HasMaxLength(100);
        builder.Property(x => x.PostalCode).HasMaxLength(100);

    }
}
public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionId);
        builder
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.Id)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.Category).HasMaxLength(255);
        builder.Property(t => t.Amount).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(255);
        builder.Property(t => t.Date).IsRequired();
    }
}
