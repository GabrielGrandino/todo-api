using MediatR;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Commands;

public class CreateTaskCommand : IRequest<TaskDTO>
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}
