using Domain;

namespace Infrastructure.EntityFramework;

public class EfTodoRepository: ITodoRepository
{
    private readonly TodoContext _context;
    
    //Constructeur
    public EfTodoRepository(TodoContext context) { _context = context; }

    public List<Todo> GetAll()
    {
        return _context.Todos.ToList();
    }

    public Todo? Get(int id)
    {
        return _context.Todos.FirstOrDefault(todo => todo.Id == id);
    }

    public Todo Create(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return new Todo { Id = todo.Id, Title = todo.Title, IsDone = todo.IsDone };
    }

    public bool Update(Todo todo)
    {
        var entity = _context.Todos.FirstOrDefault(e => e.Id == todo.Id);

        if (entity == null) { return false; }

        entity.Title = todo.Title;
        entity.IsDone = todo.IsDone;

        _context.SaveChanges();
        
        return true;
    }

    public bool Delete(int id)
    { 
        var entity = _context.Todos.FirstOrDefault(e => e.Id == id);

        if (entity == null) { return false; }

        _context.Todos.Remove(entity);
        _context.SaveChanges();
        
        return true;
    }
}