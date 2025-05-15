using Npgsql;

namespace Infrastructure.Date;


public class DataContext
{
    private const string ConnectionString = "Server=localhost; Database=library; User Id=postgres; Password=35709120";

    public Task<NpgsqlConnection> GetConnection()
    {
        return Task.FromResult(new NpgsqlConnection(ConnectionString));
    }

}
