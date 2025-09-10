using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
