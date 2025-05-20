using System.Net;
using Dapper;
using Domain.ApiResponsice;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;
using Npgsql;

namespace Infrastructure.Services;

public class BooksServices(DataContext context) : IBooksServices
{
    public async Task<Response<int>> AddBooksAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"insert into books (title, genre, publicationyear, availabcopies)
                    values (@title, @genre, @publicationyear, @availabcopies)";
        var result = await connection.ExecuteAsync(cmd, books);
        return result == 0 ? new Response<int>("Book not found", HttpStatusCode.NotFound)
        : new Response<int>(result, "Book succesfully retrieved");
    }

    public async Task<Response<List<Books>>> WatchesBooksAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from books";
        var result = await connection.QueryAsync<Books>(cmd);
        return result == null ? new Response<List<Books>>("Book not found", HttpStatusCode.NotFound)
        : new Response<List<Books>>(result.ToList(), "Book retrieved succesfully");
    }

    public async Task<Response<Books?>> GetBooksByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from books
                    where bookid = @bookid";
        var result = await connection.QuerySingleOrDefaultAsync<Books>(cmd, new { bookid = id });
        return result == null ? new Response<Books?>("Book not found", HttpStatusCode.NotFound)
        : new Response<Books?>(result, "Book succesfully retrieved ");
    }

    public async Task<Response<bool>> UpdateBooksAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"update books
                    set title = @title,
                    genre = @genre,
                    publicationyear = @publicationyear,
                    totalcopies = @totalcopies,
                    availabcopies = @availabcopies
                    where bookid = @bookid";
        var result = await connection.ExecuteAsync(cmd, books);
        return result == 0 ? new Response<bool>("Book not found", HttpStatusCode.NotFound)
        : new Response<bool>(true, "Book succesfully retrieved");
    }

    public async Task<Response<bool>> DeleteBooksAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"delete from books
                    where bookid = @bookid
                    and not exists (
                    select 1 from borrowings
                    where borrowings.bookid = books.bookid
                    and returndate IS NULL)";
        var result = await connection.ExecuteAsync(cmd, books);

        return result == 0 ? new Response<bool>("Book not found", HttpStatusCode.NotFound)
        : new Response<bool>(true, "Book succesfully retrieved");
    }

    //1
    public async Task<Response<Books?>> PopularBookAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select b.*
                    from books b
                    join borrowings br on b.bookid = br.bookId
                    group by b.bookid, b.title, b.genre, b.publicationyear, b.availabcopies
                    order by count(br.borrowingId) desc
                    limit 1;";
        var result = await connection.QuerySingleOrDefaultAsync<Books?>(cmd, new { bookId = id });
        return result == null ? new Response<Books?>("Book not found", HttpStatusCode.NotFound)
        : new Response<Books?>(result, "Book succesfully retrieverd");
    }

    //5
    public async Task<Response<List<Books>>> BookNotInMembersAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select b.* from books b
                    join borrowings br on b.bookid = br.bookid
                    where returndate is  null";
        var result = await connection.QueryAsync<Books>(cmd);
        return result == null ? new Response<List<Books>>("Book not found", HttpStatusCode.NotFound)
        : new Response<List<Books>>(result.ToList(), "Book succesfully retrieverd");

    }

    //6
    public async Task<Response<List<Books>>> BooksNotAvailableCopiesAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select b.* from books b
                    where totalcopies <= (select count(*)
                    from borrowings br
                    where br.bookid = b.bookid and br.returndate is null)";
        var result = await connection.QueryAsync<Books>(cmd);
        return result == null ? new Response<List<Books>>("Book not found", HttpStatusCode.NotFound)
        : new Response<List<Books>>(result.ToList(), "Book succesfully found");
    }

    //7
    public async Task<Response<int>> BooksNotGetAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select count(*) from books b
                        left join borrowings br on br.bookid = b.bookid
                        where br.bookid is null";
        var result = await connection.ExecuteScalarAsync<int>(cmd, books);
        return result == 0 ? new Response<int>("Book not found", HttpStatusCode.NotFound)
        : new Response<int>(result, "Book succesfully retrieverd");
    }
    //9
    public async Task<Response<string>> PopularGenreAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select  b.genre from books b
                    join borrowings br on br.bookid =  b.bookid
                    group by b.genre
                    order by  count(br.bookid) desc
                    limit 1";
        var result = await connection.QuerySingleOrDefaultAsync<string>(cmd);
        return result == null ? new Response<string>("Book not found", HttpStatusCode.NotFound)
        : new Response<string>(result, message:  "Book succesfully retrieved");
    }
}
