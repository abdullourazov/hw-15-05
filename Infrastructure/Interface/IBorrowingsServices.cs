using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBorrowingsServices
{
    Task<List<Borrowings>> GetAllBorrowingsAsync();
    Task<Borrowings?> GetBorrowingsByMemberIdAsync(int memberId);
    Task<string> CreateBorrowingAsync(Borrowings borrowings);
    Task<string> ReturnBookAsync(int BorrowingId);
    Task<int> GetAllCountBorrowingsAsync(int id);
    Task<decimal> AvgShtrafSrokAsync(int id);

}
