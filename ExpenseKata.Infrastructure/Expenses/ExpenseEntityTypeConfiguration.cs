using System;
using ExpenseKata.Domain.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseKata.Infrastructure.Expenses
{
    internal class ExpenseEntityTypeConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expense");

            builder.HasKey(x => x.Id);

            builder.Property<DateTime>("_expenseDate").HasColumnName("ExpenseDate");
            builder.Property<string>("_comment").HasColumnName("Comment");
            builder.Property<Currency>("_currency").HasColumnName("Currency")
                .HasConversion(v => v.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
            builder.Property<ExpenseNature>("_currency").HasColumnName("ExpenseNature")
                .HasConversion(v => v.ToString(), v => (ExpenseNature) Enum.Parse(typeof(ExpenseNature), v));

            builder.OwnsOne<ExpenseAmount>("_expenseAmount", "_expenseAmount", b =>
            {
                b.Property(p => p.Amount).HasColumnName("Amount");
            });

            builder.OwnsOne<ExpenseUser>("_expenseAmount", "_expenseAmount", b =>
            {
                b.Property<Guid>("_userId").HasColumnName("UserId");
            });

        }
    }
}