using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Commands;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDTO?>
{
    private readonly AppDbContext _context;
    private readonly ILogger<UpdateTaskCommandHandler> _logger;

    public UpdateTaskCommandHandler(AppDbContext context, ILogger<UpdateTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaskDTO?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando atualização da tarefa: {Titulo}", request.Titulo);
        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return null;

            if (request.Titulo != null)
                task.Title = request.Titulo;

            if (request.Descricao != null)
                task.Description = request.Descricao;

            if (request.Concluida.HasValue)
                task.IsCompleted = request.Concluida.Value;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Tarefa atualizada com sucesso. Id: {Id}", task.Id);

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
            _logger.LogError(ex, "Erro ao atualizar tarefa: {Id}", request.Id);
            throw;
        }
    }
}
