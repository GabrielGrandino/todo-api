using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.DTOs;

public class CreateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }
}
