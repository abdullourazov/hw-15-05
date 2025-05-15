using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]

public class BooksControllers : ControllerBase
{
    private IBooksServices booksServices = new BooksServices();

    [HttpPost]
    public async Task<int> AddBooksAsync(Books books)
    {
        var result = await booksServices.AddBooksAsync(books);
        return result;
    }

    [HttpGet]
    public async Task<List<Books>> WatchesBooksAsync()
    {
        var result = await booksServices.WatchesBooksAsync();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Books?> GetBooksAsyncById(int id)
    {
        var result = await booksServices.GetBooksByIdAsync(id);
        return result;
    }

    [HttpPut]
    public async Task<bool> UpdateBooksAsync(Books books)
    {
        var result = await booksServices.UpdateBooksAsync(books);
        return result;
    }

    [HttpDelete]
    public async Task<bool> DeleteBooksAsync(Books books)
    {
        var result = await booksServices.DeleteBooksAsync(books);
        return result;
    }
}
