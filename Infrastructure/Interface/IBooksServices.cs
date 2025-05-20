using Domain.ApiResponsice;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface IBooksServices
{
    Task<Response<int>> AddBooksAsync(Books books);
    Task<Response<List<Books>>> WatchesBooksAsync();
    Task<Response<Books?>> GetBooksByIdAsync(int id);
    Task<Response<bool>> UpdateBooksAsync(Books books);
    Task<Response<bool>> DeleteBooksAsync(Books books);
    Task<Response<Books?>> PopularBookAsync(int id);
    Task<Response<List<Books>>> BookNotInMembersAsync();
    Task<Response<List<Books>>> BooksNotAvailableCopiesAsync();
    Task<Response<int>> BooksNotGetAsync(Books books);
    Task<Response<string>> PopularGenreAsync();
}
