using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.DTOs;
using TodoList.Api.Features.Tasks.Commands;
using TodoList.Api.Features.Tasks.Queries;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retorna a lista de tarefas com paginação e filtro por status
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TaskDTO>>> GetTasks(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool? concluida = null)
    {
        var query = new GetTasksQuery
        {
            Page = page,
            PageSize = pageSize,
            Concluida = concluida
        };

        var tasks = await _mediator.Send(query);

        return Ok(tasks);
    }

    /// <summary>
    /// Retorna a lista de tarefas por Id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetTaskById(int id)
    {
        var query = new GetTaskByIdQuery(id);
        var task = await _mediator.Send(query);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    /// <summary>
    /// Adiciona tarefa a lista de afazeres
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateTaskCommand
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao
        };

        var task = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    /// <summary>
    /// Atualiza informações das tarefas
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDTO>> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new UpdateTaskCommand
        {
            Id = id,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Concluida = dto.Concluida
        };

        var updatedTask = await _mediator.Send(command);

        if (updatedTask == null)
            return NotFound();

        return Ok(updatedTask);
    }

    /// <summary>
    /// Deleta Tarefa pelo ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteTaskCommand(id);
        var deleted = await _mediator.Send(command);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
