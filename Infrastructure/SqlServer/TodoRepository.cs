using System.Data;
using Domain;

namespace Infrastructure.SqlServer;

public class TodoRepository: ITodoRepository
{
    private readonly Database _database;
    private readonly TodoFactory _todoFactory;

    public static string TableName = "items", ColumnId = "id", ColumnTitle = "title", ColumnIsDone = "is_done";

    private static string 
        _reqGetAll = $"SELECT * FROM {TableName}", 
        _reqGetById = $"SELECT * FROM {TableName} WHERE {ColumnId} = @{ColumnId}",
        _reqCreate = $"INSERT INTO {TableName} ({ColumnTitle}, {ColumnIsDone}) "
            + $"OUTPUT Inserted.{ColumnId} "
            + $"VALUES(@{ColumnTitle}, @{ColumnIsDone})",
        _reqUpdate = $"UPDATE {TableName} SET {ColumnTitle} = @{ColumnTitle}, {ColumnIsDone} = @{ColumnIsDone} "
            + $"WHERE {ColumnId} = @{ColumnId}",
        _reqDelete = $"DELETE FROM {TableName} WHERE {ColumnId} = @{ColumnId}";
    
    public TodoRepository(Database database, TodoFactory todoFactory)
    {
        _database = database;
        _todoFactory = todoFactory;
    }

    public List<Todo> GetAll()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = _reqGetAll;

        List<Todo> todos = new List<Todo>();

        var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (reader.Read())
        {
            var todo = _todoFactory.FromReader(reader);
            
            todos.Add(todo);
        }

        return todos;   
    }

    public Todo? Get(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = _reqGetById;

        command.Parameters.AddWithValue("@" + ColumnId, id);

        var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        return reader.Read() ? _todoFactory.FromReader(reader) : null;
    }

    public Todo Create(Todo todo)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = _reqCreate;
        
        command.Parameters.AddWithValue("@" + ColumnTitle, todo.Title);
        command.Parameters.AddWithValue("@" + ColumnIsDone, todo.IsDone);

        return new Todo
        {
            Id = (int)command.ExecuteScalar(),
            Title = todo.Title,
            IsDone = todo.IsDone
        };
    }

    public bool Update(Todo todo)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = _reqUpdate;
        
        command.Parameters.AddWithValue("@" + ColumnId, todo.Id);
        command.Parameters.AddWithValue("@" + ColumnTitle, todo.Title);
        command.Parameters.AddWithValue("@" + ColumnIsDone, todo.IsDone);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = _reqDelete;
        
        command.Parameters.AddWithValue("@" + ColumnId, id);
        
        return command.ExecuteNonQuery() == 1;
    }
}