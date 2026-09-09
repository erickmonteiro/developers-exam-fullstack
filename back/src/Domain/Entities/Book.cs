using FluentValidation;

namespace Domain.Entities;

public class Book : Entity<Book>
{
	public string Title { get; private set; } = "";
	public string Author { get; private set; } = "";
	public string Description { get; private set; } = "";

	// Construtor protegido para o EF Core
	protected Book()
	{
	}

	public Book(string title, string author, string description)
	{
		Title = title?.Trim() ?? "";
		Author = author?.Trim() ?? "";
		Description = description?.Trim() ?? "";

		SetLastAction();
		Validate();
	}

	public void Update(string title, string author, string description)
	{
		Title = title?.Trim() ?? "";
		Author = author?.Trim() ?? "";
		Description = description?.Trim() ?? "";

		SetLastAction();
		Validate();
	}

	public override bool IsValid()
	{
		Validate();

		return ValidationResult.IsValid;
	}

	private void Validate()
	{
		RuleFor(x => x.Title).NotEmpty().WithMessage("O título é obrigatório.").Length(10, 100).WithMessage("O título deve ter entre 10 e 100 caracteres.");
		RuleFor(x => x.Author).NotEmpty().WithMessage("O autor é obrigatório.").Length(10, 100).WithMessage("O autor deve ter entre 10 e 100 caracteres.");
		RuleFor(x => x.Description).MaximumLength(1024).WithMessage("A descrição pode ter no máximo 1024 caracteres.");

		ValidationResult = Validate(this);
	}
}