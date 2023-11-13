using Domain;

namespace Infrastructure;

public interface ITodoRepository
{
    List<Todo> GetAll();
    Todo? Get(int id);
    Todo Create(Todo todo);
    bool Update(Todo todo);
    bool Delete(int id);
}