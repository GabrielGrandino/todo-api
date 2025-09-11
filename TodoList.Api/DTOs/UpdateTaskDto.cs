namespace TodoList.Api.DTOs;

public class UpdateTaskDto
{
    public string? Titulo { get; set; }  
    public string? Descricao { get; set; }
    public bool? Concluida { get; set; }    
}