using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Commands;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDTO?>
{
    private readonly AppDbContext _context;

    public UpdateTaskCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDTO?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task == null)
            return null;

        // Atualiza apenas os campos que vieram
        if (request.Titulo != null)
            task.Title = request.Titulo;

        if (request.Descricao != null)
            task.Description = request.Descricao;

        if (request.Concluida.HasValue)
            task.IsCompleted = request.Concluida.Value;

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
