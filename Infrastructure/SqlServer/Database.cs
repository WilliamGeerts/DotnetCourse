using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.SqlServer;

public class Database
{
    private readonly IConfiguration _configuration;

    public Database(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Todo"));
    }
}