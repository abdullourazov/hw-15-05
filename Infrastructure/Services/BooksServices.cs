using Dapper;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class BooksServices : IBooksServices
{
    private readonly DataContext context = new();
    public async Task<int> AddBooksAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"insert into books (title, genre, publicationyear, availabcopies)
                    values (@title, @genre, @publicationyear, @availabcopies)";
        var result = await connection.ExecuteAsync(cmd, books);
        return result;
    }

    public async Task<List<Books>> WatchesBooksAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from books";
        var result = await connection.QueryAsync<Books>(cmd);
        return result.ToList();
    }

    public async Task<Books?> GetBooksByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from books
                    where bookid = @bookid";
        var result = await connection.QuerySingleOrDefaultAsync<Books>(cmd, new { bookid = id });
        return result;
    }

    public async Task<bool> UpdateBooksAsync(Books books)
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
        return result > 0;
    }
    public async Task<bool> DeleteBooksAsync(Books books)
    {
        using var connection = await context.GetConnection();
        var cmd = @"delete from books
                    where bookid = @bookid
                    and not exists (
                    select 1 from borrowings
                    where borrowings.bookid = books.bookid
                    and returndate IS NULL)";
        var result = await connection.ExecuteAsync(cmd, books);
        return result > 0;
    }

    //1
    public async Task<Books?> PopularBook(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select b.*
                    from books b
                    join borrowings br on b.bookid = br.bookId
                    group by b.bookid, b.title, b.genre, b.publicationyear, b.availabcopies
                    order by count(br.borrowingId) desc
                    limit 1;";
        var result = await connection.QuerySingleOrDefaultAsync<Books?>(cmd, new { bookId = id });
        return result;
    }
    
        

}
