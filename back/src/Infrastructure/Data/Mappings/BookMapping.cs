using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class BookMapping : EntityTypeConfiguration<Book>
{
	public override void Configure(EntityTypeBuilder<Book> builder)
	{
		base.Configure(builder);

		builder.ToTable("Books");

		builder.Property(x => x.Title).IsRequired().HasMaxLength(100);

		builder.HasIndex(x => x.Title).IsUnique();

		builder.Property(x => x.Author).IsRequired().HasMaxLength(100);

		builder.Property(x => x.Description).HasMaxLength(1024);
	}
}