using Domain.ApiResponsice;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[Controller]")]

public class BooksControllers(IBooksServices booksServices)
{

    [HttpPost]
    public async Task<Response<int>> AddBooksAsync(Books books)
    {
        var result = await booksServices.AddBooksAsync(books);
        return result;
    }

    [HttpGet]
    public async Task<Response<List<Books>>> WatchesBooksAsync()
    {
        var result = await booksServices.WatchesBooksAsync();
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<Books?>> GetBooksAsyncById(int id)
    {
        var result = await booksServices.GetBooksByIdAsync(id);
        return result;
    }

    [HttpPut]
    public async Task<Response<bool>> UpdateBooksAsync(Books books)
    {
        var result = await booksServices.UpdateBooksAsync(books);
        return result;
    }

    [HttpDelete]
    public async Task<Response<bool>> DeleteBooksAsync(Books books)
    {
        var result = await booksServices.DeleteBooksAsync(books);
        return result;
    }

    [HttpGet("popular/{id}")]
    public async Task<Response<Books?>> PopularBookAsync(int id)
    {
        return await booksServices.PopularBookAsync(id);
    }

    [HttpGet("spisok")]
    public async Task<Response<List<Books>>> BookNotInMembersAsync()
    {
        return await booksServices.BookNotInMembersAsync();
    }

    [HttpGet("not available")]
    public async Task<Response<List<Books>>> BooksNotAvailableCopiesAsync()
    {
        return await booksServices.BooksNotAvailableCopiesAsync();
    }

    [HttpGet("book not neg")]
    public async Task<Response<int>> BooksNotGetAsync(Books books)
    {
        return await booksServices.BooksNotGetAsync(books);
    }

    [HttpGet("popular genre")]
    public async Task<Response<string>> PopularGenreAsync()
    {
        return await booksServices.PopularGenreAsync();
    }




}
