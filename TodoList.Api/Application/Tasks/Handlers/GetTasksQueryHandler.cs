using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.DTOs;

namespace TodoList.Api.Features.Tasks.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDTO>>
{
    private readonly AppDbContext _context;

    public GetTasksQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDTO>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        int page = request.Page < 1 ? 1 : request.Page;
        int pageSize = request.PageSize < 1 ? 10 : (request.PageSize > 100 ? 100 : request.PageSize);

        var query = _context.Tasks.AsQueryable();

        if (request.Concluida.HasValue)
            query = query.Where(t => t.IsCompleted == request.Concluida.Value);

        query = query.OrderByDescending(t => t.CreatedAt)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize);

        var tasks = await query.ToListAsync(cancellationToken);

        return tasks.Select(t => new TaskDTO
        {
            Id = t.Id,
            Titulo = t.Title,
            Descricao = t.Description,
            DataCriacao = t.CreatedAt,
            Concluida = t.IsCompleted
        }).ToList();
    }
}
