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
    public DbSet<Category> Categories => Set<Category>();

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

            builder.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(builder =>
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasData(
                new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Alimentación" },
                new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Transporte" },
                new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Casa" },
                new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Salud" },
                new Category { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Ocio" },
                new Category { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Otros" }
            );
        });
    }
}
