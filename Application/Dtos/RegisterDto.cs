using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public class RegisterDto
{
    [MaxLength(50)] 
    public required string Email { get; set; }
    [MaxLength(40)] 
    public required string Password { get; set; }
}