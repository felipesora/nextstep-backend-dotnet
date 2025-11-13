using System.ComponentModel.DataAnnotations;

namespace NS.Application.Dtos;

public class UsuarioDTO
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
}
