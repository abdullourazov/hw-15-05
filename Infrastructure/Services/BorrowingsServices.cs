using Dapper;
using Domain.Entities;
using Infrastructure.Date;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class BorrowingsServices(DataContext context) : IBorrowingsServices
{
    public async Task<List<Borrowings>> GetAllBorrowingsAsync()
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from borrowings";
        var result = await connection.QueryAsync<Borrowings>(cmd);
        return result.ToList();
    }

    public async Task<Borrowings?> GetBorrowingsByMemberIdAsync(int memberId)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select * from borrowings 
                    memberId = @memberId";
        var result = await connection.QueryFirstOrDefaultAsync<Borrowings?>(cmd, new { memberId = memberId });
        return result;
    }

    public async Task<string> CreateBorrowingAsync(Borrowings borrowings)
    {
        using var connection = await context.GetConnection();
        var bookCommand = @"select * from books 
                            where BookId = @BookId";
        var book = await connection.QueryFirstOrDefaultAsync<Books>(bookCommand, new { BorrowingId = borrowings.BorrowingId });

        if (book == null)
        {
            return "Book not found";
        }

        if (book.Availabcopies <= 0)
        {
            return "Available copies are not available";
        }

        if (borrowings.BorrowDate >= borrowings.DueDate)
        {
            return "Borrowing due date is earlier";
        }

        var borrowingCommand = @"insert into borrowings(BookId, memberId, BorrowDate, DueDate)
                                values (@BookId, memberId, @BorrowDate, @DueDate)";
        var result = await connection.ExecuteAsync(borrowingCommand, borrowings);
        if (result == 0)
        {
            return "Borrowing not created";
        }

        var updateBookCommand = @"update books set Availabcopies = Availabcopies - 1
                                  where id = @id";
        await connection.ExecuteAsync(updateBookCommand, new { id = borrowings.BookId });
        return "Borrowing created";
    }

    public async Task<string> ReturnBookAsync(int BorrowingId)
    {
        using var connection = await context.GetConnection();
        var borrowingCommand = @"select * from borrowings 
                                where BorrowingId = @BorrowingId";
        var borrowing = await connection.QueryFirstOrDefaultAsync<Borrowings>(borrowingCommand, new { id = BorrowingId });
        if (borrowing == null)
        {
            return "Borrowing not found";
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
            return "Borrowing not updated";
        }

        var updateBookCommand = @"update books 
                                set availabcopies = @availabcopies + 1
                                where bookid = @bookid";
        await connection.ExecuteAsync(updateBookCommand, new { id = borrowing.BorrowingId });
        return "Borrowing updated";
    }

    //3
    public async Task<int> GetAllCountBorrowingsAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select br.bookid, count(*) from borrowings br
                    join books b on b.bookid = br.borrowingid
                    group by  br.bookid";
        var result = await connection.ExecuteScalarAsync<int>(cmd, new { borrowingid = id });
        return result;
    }

    //4
    public async Task<decimal> AvgShtrafSrokAsync(int id)
    {
        using var connection = await context.GetConnection();
        var cmd = @"select avg(fine)
                    from borrowings";
        var result = await connection.ExecuteScalarAsync<decimal>(cmd, new { borrowingid = id });
        return result;
    }






}
