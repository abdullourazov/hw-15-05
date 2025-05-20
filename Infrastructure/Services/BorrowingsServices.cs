using System.Net;
using Dapper;
using Domain.ApiResponsice;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class BorrowingsServices(DataContext context) : IBorrowingsServices
{
    public async Task<Response<List<Borrowings>>> GetAllBorrowingsAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from borrowings";
        var result = await connection.QueryAsync<Borrowings>(cmd);
        return result == null ? new Response<List<Borrowings>>("Borrowings not found", HttpStatusCode.NotFound)
        : new Response<List<Borrowings>>(result.ToList(), "Borrowing succesfully retrivered");
    }

    public async Task<Response<Borrowings?>> GetBorrowingsByMemberIdAsync(int memberId)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from borrowings 
                    memberId = @memberId";
        var result = await connection.QueryFirstOrDefaultAsync<Borrowings?>(cmd, new { memberId = memberId });
        return result == null ? new Response<Borrowings?>("Borrrowings not found", HttpStatusCode.NotFound)
        : new Response<Borrowings?>(result, "Borrowings succesfully retrivered");
    }

    public async Task<Response<string>> CreateBorrowingAsync(Borrowings borrowings)
    {
        using var connection = await context.GetConnection();
        var bookCommand = @"select * from books 
                            where BookId = @BookId";
        var book = await connection.QueryFirstOrDefaultAsync<Books>(bookCommand, new { BorrowingId = borrowings.BorrowingId });

        if (book == null)
        {
            return new Response<string>("Borrowing not found", HttpStatusCode.NotFound);
        }

        if (book.Availabcopies <= 0)
        {
            return new Response<string>("Available copies are not available", HttpStatusCode.NotFound);
        }

        if (borrowings.BorrowDate >= borrowings.DueDate)
        {
            return new Response<string>("Borrowing due date is earlier", HttpStatusCode.NotFound);
        }

        var borrowingCommand = @"insert into borrowings(BookId, memberId, BorrowDate, DueDate)
                                values (@BookId, memberId, @BorrowDate, @DueDate)";
        var result = await connection.ExecuteAsync(borrowingCommand, borrowings);
        if (result == 0)
        {
            return new Response<string>("Borrowing not created", HttpStatusCode.NotFound);
        }

        var updateBookCommand = @"update books set Availabcopies = Availabcopies - 1                    
                                  where id = @id";
        await connection.ExecuteAsync(updateBookCommand, new { id = borrowings.BookId });
        return new Response<string>(result.ToString(), message: "Borrowing created");
    }

    public async Task<Response<string>> ReturnBookAsync(int BorrowingId)
    {
        using var connection = await context.GetConnection();
        var borrowingCommand = @"select * from borrowings 
                                where BorrowingId = @BorrowingId";
        var borrowing = await connection.QueryFirstOrDefaultAsync<Borrowings>(borrowingCommand, new { id = BorrowingId });
        if (borrowing == null)
        {
            return new Response<string>("Borrowing not found", HttpStatusCode.NotFound);
        }

        borrowing.ReturnDate = DateTime.Now;
        if (borrowing.ReturnDate > borrowing.DueDate)
        {
            var days = borrowing.ReturnDate.Value.Day - borrowing.DueDate.Day;
            borrowing.Fine = days * 10;
        }

        var updateBorrowingCommand = @"update borrowing
                                        set returndate = @returndate,
                                        Fine = @Fine
                                        where BorrowingId = @BorrowingId";
        var result = await connection.ExecuteAsync(updateBorrowingCommand, borrowing);
        if (result == 0)
        {
            return new Response<string>("Borrowing not updated", HttpStatusCode.NotFound);
        }

        var updateBookCommand = @"update books 
                                set availabcopies = @availabcopies + 1
                                where bookid = @bookid";
        await connection.ExecuteAsync(updateBookCommand, new { id = borrowing.BorrowingId });
        return new Response<string>(result.ToString(), message: "Borrowing updated");

    }

    //3
    public async Task<Response<int>> GetAllCountBorrowingsAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select br.bookid, count(*) from borrowings br
                    join books b on b.bookid = br.borrowingid
                    group by  br.bookid";
        var result = await connection.ExecuteScalarAsync<int>(cmd, new { borrowingid = id });
        return result == 0 ? new Response<int>("Borrowings not found", HttpStatusCode.NotFound)
        : new Response<int>(result, "Borrowings succesfullly retrivered");
    }

    //4
    public async Task<Response<decimal>> AvgShtrafSrokAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select avg(fine)
                    from borrowings";
        var result = await connection.ExecuteScalarAsync<decimal>(cmd, new { borrowingid = id });
        return result == 0 ? new Response<decimal>("Borrowings not found", HttpStatusCode.NotFound)
        : new Response<decimal>(result, "Borrowings succesfully retriveres");
    }

}
