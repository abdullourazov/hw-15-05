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

    [HttpGet("popular/{id}")]
    public async Task<Books?> PopularBookAsync(int id)
    {
        return await booksServices.PopularBookAsync(id);
    }

    [HttpGet("spisok")]
    public async Task<List<Books>> BookNotInMembersAsync()
    {
        return await booksServices.BookNotInMembersAsync();
    }

    [HttpGet("not available")]
    public async Task<List<Books>> BooksNotAvailableCopiesAsync()
    {
        return await booksServices.BooksNotAvailableCopiesAsync();
    }

    [HttpGet("book not neg")]
    public async Task<int> BooksNotGetAsync(Books books)
    {
        return await booksServices.BooksNotGetAsync(books);
    }

    [HttpGet("popular genre")]
    public async Task<string> PopularGenreAsync()
    {
        return await booksServices.PopularGenreAsync();
    }




}
