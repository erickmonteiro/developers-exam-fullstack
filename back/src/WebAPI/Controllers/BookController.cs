using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.Common;
using WebAPI.DTOs.Requests;
using WebAPI.DTOs.Responses;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
	private readonly IRepository<Book> _bookRepository;
	private readonly SqlDbContext _context;

	public BookController(IRepository<Book> bookRepository, SqlDbContext context)
	{
		_bookRepository = bookRepository;
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<PagedResponse<BookResponse>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		if (page < 1)
		{
			page = 1;
		}

		if (pageSize < 1)
		{
			pageSize = 10;
		}

		var totalItems = await _context.Books.CountAsync();
		var books = await _context.Books
			.AsNoTracking()
			.OrderByDescending(b => b.Id)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.Select(b => new BookResponse(
					b.Id,
					b.Title,
					b.Author,
					b.Description,
					b.CreatedDate,
					b.LastUpdatedDate
				)
			)
			.ToListAsync();

		var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

		return Ok(new PagedResponse<BookResponse>(books, page, pageSize, totalItems, totalPages));
	}

	[HttpGet("{id:long}")]
	public async Task<ActionResult<BookResponse>> GetById(long id)
	{
		var book = await _bookRepository.GetByIdAsync(id);

		if (book is null)
		{
			return NotFound(new { message = "Livro não encontrado." });
		}

		var response = new BookResponse(
			book.Id,
			book.Title,
			book.Author,
			book.Description,
			book.CreatedDate,
			book.LastUpdatedDate
		);

		return Ok(response);
	}

	[HttpPost]
	public async Task<ActionResult<BookResponse>> Create([FromBody] BookRequest request)
	{
		var normalizedTitle = request.Title?.Trim().ToLower() ?? "";
		var exists = await _context.Books.AnyAsync(b => b.Title.ToLower() == normalizedTitle);

		if (exists)
		{
			return BadRequest(new { errors = new[] { "Já existe um livro cadastrado com este título." } });
		}

		var book = new Book(request.Title!, request.Author!, request.Description ?? "");

		if (!book.IsValid())
		{
			return BadRequest(new { errors = book.ValidationResult.Errors.Select(e => e.ErrorMessage) });
		}

		await _bookRepository.InsertAsync(book);

		var response = new BookResponse(
			book.Id,
			book.Title,
			book.Author,
			book.Description,
			book.CreatedDate,
			book.LastUpdatedDate
		);

		return CreatedAtAction(nameof(GetById), new { id = book.Id }, response);
	}

	[HttpPut("{id:long}")]
	public async Task<IActionResult> Update(long id, [FromBody] BookRequest request)
	{
		var book = await _bookRepository.GetByIdAsync(id);

		if (book is null)
		{
			return NotFound(new { message = "Livro não encontrado." });
		}

		var normalizedTitle = request.Title?.Trim().ToLower() ?? "";
		var exists = await _context.Books.AnyAsync(b => b.Id != id && b.Title.ToLower() == normalizedTitle);

		if (exists)
		{
			return BadRequest(new { errors = new[] { "Já existe outro livro cadastrado com este título." } });
		}

		book.Update(request.Title!, request.Author!, request.Description);

		if (!book.IsValid())
		{
			return BadRequest(new { errors = book.ValidationResult.Errors.Select(e => e.ErrorMessage) });
		}

		await _bookRepository.UpdateAsync(book);

		return NoContent();
	}

	[HttpDelete("{id:long}")]
	public async Task<IActionResult> Delete(long id)
	{
		var book = await _bookRepository.GetByIdAsync(id);
		if (book is null)
		{
			return NotFound(new { message = "Livro não encontrado." });
		}

		await _bookRepository.DeleteAsync(id);

		return NoContent();
	}
}