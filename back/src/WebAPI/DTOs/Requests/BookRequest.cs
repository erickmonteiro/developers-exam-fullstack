using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Requests;

public record BookRequest(
	[Required(ErrorMessage = "O título é obrigatório.")]
	string Title,

	[Required(ErrorMessage = "O autor é obrigatório.")]
	string Author,

	string? Description
);