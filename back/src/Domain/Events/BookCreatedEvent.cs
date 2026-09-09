using Domain.Entities;

namespace Domain.Events;

public class BookCreatedEvent : DomainEvent
{
	public Book Book { get; }
	public string TargetEmail => "developers@inspand.com.br";

	public BookCreatedEvent(Book book)
	{
		Book = book;
	}
}