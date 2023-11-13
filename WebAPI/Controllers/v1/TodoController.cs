using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Dtos;

namespace WebAPI.Controllers.v1;

[ApiController]
[Route("api/v1/todos")]
public class TodoController: ControllerBase
{
    private readonly ITodoRepository _todoRepository;

    public TodoController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Todo>> getAll()
    {
        return Ok(_todoRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todoRepository.Get(id);

        if (todo == null)
            return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Todo> Create(DtosInputCreateTodo dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            IsDone = dto.IsDone
        };
        
        return StatusCode(201, _todoRepository.Create(todo));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(DtosInputUpdateTodo dto)
    {
        var todo = new Todo
        {
            Id = dto.Id,
            Title = dto.Title,
            IsDone = dto.IsDone
        };

        if (_todoRepository.Update(todo))
            return NoContent();
        return NotFound();
        {
            
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        if (_todoRepository.Delete(id))
            return NoContent();
        return NotFound();
    }
}