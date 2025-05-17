using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBorrowingsServices
{
    Task<List<Borrowings>> GetAllBorrowingsAsync();
    Task<Borrowings?> GetBorrowingsByMemberId(int memberId);
    Task<string> CreateBorrowingAsync(Borrowings borrowings);
    Task<string> ReturnBookAsync(int BorrowingId);
    Task<int> GetAllCountBorrowings(int id);
    Task<decimal> AvgShtrafSrok(int id);

}
