using System.Data.SqlClient;
using Domain;

namespace Infrastructure.SqlServer;

public class TodoFactory
{
    public Todo FromReader(SqlDataReader reader)
    {
        return new Todo
        {
            Id = reader.GetInt32(reader.GetOrdinal(TodoRepository.ColumnId)),
            Title = reader.GetString(reader.GetOrdinal(TodoRepository.ColumnTitle)),
            IsDone = reader.GetBoolean(reader.GetOrdinal(TodoRepository.ColumnIsDone))
        };
    }
}