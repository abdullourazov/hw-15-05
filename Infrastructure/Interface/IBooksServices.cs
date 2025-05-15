using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBooksServices
{
    Task<int> AddBooksAsync(Books books);
    Task<List<Books>> WatchesBooksAsync();
    Task<Books?> GetBooksByIdAsync(int id);
    Task<bool> UpdateBooksAsync(Books books);
    Task<bool> DeleteBooksAsync(Books books);
}
