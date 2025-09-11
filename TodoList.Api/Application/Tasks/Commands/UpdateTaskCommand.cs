using MediatR;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Commands;

public class UpdateTaskCommand : IRequest<TaskDTO?>
{
    public int Id { get; set; }
    public string? Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool? Concluida { get; set; }
}
