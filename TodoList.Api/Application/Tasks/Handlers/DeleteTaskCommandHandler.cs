using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;

namespace TodoList.Api.Features.Tasks.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;

    public DeleteTaskCommandHandler(AppDbContext context, ILogger<DeleteTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando remoção da tarefa: {Id}", request.Id);
        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Tarefa excluída com sucesso. Id: {Id}", task.Id);

            return true;
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Erro ao excluir tarefa: {Id}", request.Id);
            throw;
        }
        
    }
}
