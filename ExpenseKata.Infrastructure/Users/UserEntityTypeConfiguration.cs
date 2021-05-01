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
                new {Id = Guid.Parse("124356D7-FC20-4DCA-AF5E-948A45BFD0E1"), FirstName = "Anthony", LastName = "Stark", Currency = Currency.Dollar},
                new {Id = Guid.Parse("8CAF7E4C-44AE-4FE6-878F-DFD2C3BF4DF3"), FirstName = "Natasha", LastName = "Romanova", Currency = Currency.RoubleRusse}
                );
        }
    }
}