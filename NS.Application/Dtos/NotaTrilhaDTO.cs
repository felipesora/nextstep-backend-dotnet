using System.ComponentModel.DataAnnotations;

namespace NS.Application.Dtos;

public class NotaTrilhaDTO
{
    [Required]
    public int ValorNota { get; set; }

    public string Observacao { get; set; }

    [Required]
    public long IdTrilha { get; set; }

    [Required]
    public long IdUsuario { get; set; }
}
