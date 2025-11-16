using NS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NS.Application.Dtos;

public class FormularioDTO
{
    [Required]
    public NivelExperiencia NivelExperiencia { get; set; }

    [Required]
    public ObjetivoCarreira ObjetivoCarreira { get; set; }

    [Required]
    public AreaTecnologia AreaTecnologia1 { get; set; }

    public AreaTecnologia? AreaTecnologia2 { get; set; }

    public AreaTecnologia? AreaTecnologia3 { get; set; }

    [Required]
    public HorasEstudo HorasEstudo { get; set; }

    public string? Habilidades { get; set; }

    [Required]
    public long IdUsuario { get; set; }
}
