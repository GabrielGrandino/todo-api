using MediatR;
using TodoList.Api.Data;
using TodoList.Api.DTOs;
using TodoList.Api.Models;

namespace TodoList.Api.Features.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDTO>
{
    private readonly AppDbContext _context;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public CreateTaskCommandHandler(AppDbContext context, ILogger<CreateTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaskDTO> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando criação da tarefa: {Titulo}", request.Titulo);

        try
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

            _logger.LogInformation("Tarefa criada com sucesso. Id: {Id}", task.Id);

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
            _logger.LogError(ex, "Erro ao criar tarefa: {Titulo}", request.Titulo);
            throw;
        }
    }
}
