using ExpenseTracker.Modules.Expenses.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Modules.Expenses.Persistence;

public sealed class ExpensesDbContext : DbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options)
       : base(options)
    {
    }

    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(builder =>
        {
            builder.ToTable("expenses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Date)
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();
        });
    }
}
