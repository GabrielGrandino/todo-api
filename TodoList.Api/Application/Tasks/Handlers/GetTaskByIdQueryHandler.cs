using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Queries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDTO?>
{
    private readonly AppDbContext _context;
    private readonly ILogger<GetTaskByIdQueryHandler> _logger;

    public GetTaskByIdQueryHandler(AppDbContext context, ILogger<GetTaskByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaskDTO?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        try
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
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Erro ao criar buscar tarefa: {Id}", request.Id);
            throw;
        }
    }
}
