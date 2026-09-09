using Bogus;
using Domain.Entities;

namespace Infrastructure.Data;

public static class DbInitializer
{
	public static void Seed(SqlDbContext context)
	{
		context.Database.EnsureCreated();

		// Roda o seed apenas se não existir dados
		if (context.Books.Any())
		{
			return;
		}

		var faker = new Faker<Book>("pt_BR").CustomInstantiator(f => new Book(
				f.Commerce.ProductName(),
				f.Name.FullName(),
				f.Lorem.Paragraph()
			)
		);

		var books = faker.Generate(25);

		context.Books.AddRange(books);
		context.SaveChanges();
	}
}