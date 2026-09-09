using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookControllerr : ControllerBase
{
    [HttpGet()]
    public IEnumerable<object> Get(uint page = 1, uint pageSize = 10)
    {
        List<object> books = [];

        for(int i = 0 ; i < 1000 ; i++)
            books.Add(new { Id = i, Title = $"Book {i}", Author = $"Author {i}", description = $"Book {i} - Description {i}" });

        return books.Skip((int)page * (int)pageSize).Take((int)pageSize);
    }
}