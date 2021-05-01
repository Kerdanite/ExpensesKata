using System;
using ExpenseKata.Domain.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseKata.Infrastructure.Expenses
{
    internal class ExpenseEntityTypeConfiguration : IEntityTypeConfiguration<ExpenseMemento>
    {
        public void Configure(EntityTypeBuilder<ExpenseMemento> builder)
        {
            builder.ToTable("Expense");

            builder.HasKey(x => x.Id);


            builder.Property(p => p.Amount).HasColumnName("Amount");
            builder.Property<DateTime>(p => p.ExpenseDate).HasColumnName("ExpenseDate");
            builder.Property<string>(p => p.Comment).HasColumnName("Comment");
            builder.Property<Currency>(p => p.Currency).HasColumnName("Currency")
                .HasConversion(v => v.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));
            builder.Property<ExpenseNature>(p => p.Nature).HasColumnName("ExpenseNature")
                .HasConversion(v => v.ToString(), v => (ExpenseNature) Enum.Parse(typeof(ExpenseNature), v));
            builder.Property<Guid>(p => p.UserId).HasColumnName("UserId");
        }
    }
}