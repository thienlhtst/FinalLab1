using FinalLab1.Entities;

namespace FinalLab1.Dtos;

public class RegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTimeOffset Birthday { get; set; }
    public Gender Gender { get; set; }
}