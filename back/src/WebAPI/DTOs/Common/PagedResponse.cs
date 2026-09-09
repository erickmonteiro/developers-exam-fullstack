namespace WebAPI.DTOs.Common;

public record PagedResponse<T>(
	IEnumerable<T> Data,
	int Page,
	int PageSize,
	int TotalItems,
	int TotalPages
);