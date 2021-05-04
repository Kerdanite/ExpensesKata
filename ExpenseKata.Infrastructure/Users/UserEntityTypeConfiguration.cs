using System;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseKata.Infrastructure.Users
{
    public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.FirstName);
            builder.Property(p => p.LastName);
            builder.Property<Currency>(p => p.Currency).HasColumnName("Currency")
                .HasConversion(v => v.ToString(), v => (Currency) Enum.Parse(typeof(Currency), v));


            builder.HasData(
                new {Id = Guid.NewGuid(), FirstName = "Anthony", LastName = "Stark", Currency = Currency.Dollar},
                new {Id = Guid.NewGuid(), FirstName = "Natasha", LastName = "Romanova", Currency = Currency.RoubleRusse}
                );
        }
    }
}