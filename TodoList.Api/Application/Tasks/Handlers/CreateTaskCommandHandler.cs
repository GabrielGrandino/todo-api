using MediatR;
using TodoList.Api.Data;
using TodoList.Api.DTOs;
using TodoList.Api.Models;

namespace TodoList.Api.Features.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDTO>
{
    private readonly AppDbContext _context;

    public CreateTaskCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDTO> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Titulo,
            Description = request.Descricao,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return new TaskDTO
        {
            Id = task.Id,
            Titulo = task.Title,
            Descricao = task.Description,
            DataCriacao = task.CreatedAt,
            Concluida = task.IsCompleted
        };
    }
}
