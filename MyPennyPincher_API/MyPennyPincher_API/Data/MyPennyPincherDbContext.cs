using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Context;

public partial class MyPennyPincherDbContext : DbContext
{
    public MyPennyPincherDbContext()
    {
    }

    public MyPennyPincherDbContext(DbContextOptions<MyPennyPincherDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Expenses__1445CFD380DD5E23");

            entity.Property(e => e.ExpenseId).ValueGeneratedOnAdd();

            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);

        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.ExpenseCategoryId).HasName("PK__ExpenseC__9C2C63F8CC70B4FC");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PK__Incomes__60DFC60C2C10493F");

            entity.Property(i => i.IncomeId).ValueGeneratedOnAdd();

            entity.Property(e => e.Source)
                .HasMaxLength(150)
                .IsUnicode(false);

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA5C04E72");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasIndex(u => u.Email)
                .IsUnique();
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__F5845E394FB8BAE0");

            entity.Property(e => e.RefreshTokenId).ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Token, "UQ__RefreshT__1EB4F817DEE02968").IsUnique();

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
