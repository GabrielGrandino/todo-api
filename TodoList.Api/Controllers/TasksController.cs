using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.DTOs;
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
}
