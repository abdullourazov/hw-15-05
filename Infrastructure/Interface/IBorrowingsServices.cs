using Domain.ApiResponsice;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBorrowingsServices
{
    Task<Response<List<Borrowings>>> GetAllBorrowingsAsync();
    Task<Response<Borrowings?>> GetBorrowingsByMemberIdAsync(int memberId);
    Task<Response<string>> CreateBorrowingAsync(Borrowings borrowings);
    Task<Response<string>> ReturnBookAsync(int BorrowingId);
    Task<Response<int>> GetAllCountBorrowingsAsync(int id);
    Task<Response<decimal>> AvgShtrafSrokAsync(int id);

}
