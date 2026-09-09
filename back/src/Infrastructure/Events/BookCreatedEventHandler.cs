using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Infrastructure.Events;

public class BookCreatedEventHandler : INotificationHandler<DomainEventNotification<BookCreatedEvent>>
{
	private readonly IEmailService _emailService;

	public BookCreatedEventHandler(IEmailService emailService)
	{
		_emailService = emailService;
	}

	public Task Handle(DomainEventNotification<BookCreatedEvent> notification, CancellationToken cancellationToken)
	{
		var domainEvent = notification.DomainEvent;

		_emailService.SendEmail(
			domainEvent.TargetEmail,
			$"O livro '{domainEvent.Book.Title}' foi cadastrado com sucesso!"
		);

		return Task.CompletedTask;
	}
}