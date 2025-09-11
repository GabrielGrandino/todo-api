using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Queries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDTO?>
{
    private readonly AppDbContext _context;

    public GetTaskByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDTO?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task == null)
            return null;

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
