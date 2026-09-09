namespace WebAPI.DTOs.Responses;

public record BookResponse(
	long Id,
	string Title,
	string Author,
	string Description,
	DateTime CreatedDate,
	DateTime? LastUpdatedDate
);