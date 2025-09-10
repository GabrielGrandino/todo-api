using MediatR;
using TodoList.Api.DTOs;
using System.Collections.Generic;

namespace TodoList.Api.Features.Tasks.Queries;

public class GetTasksQuery : IRequest<List<TaskDTO>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool? Concluida { get; set; }
}
