using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBooksServices
{
    Task<int> AddBooksAsync(Books books);
    Task<List<Books>> WatchesBooksAsync();
    Task<Books?> GetBooksByIdAsync(int id);
    Task<bool> UpdateBooksAsync(Books books);
    Task<bool> DeleteBooksAsync(Books books);
    Task<Books?> PopularBookAsync(int id);
    Task<List<Books>> BookNotInMembersAsync();
    Task<List<Books>> BooksNotAvailableCopiesAsync();
    Task<int> BooksNotGetAsync(Books books);
    Task<string> PopularGenreAsync();
}
