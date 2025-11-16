using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class FormularioMapper
{
    public static FormularioEntity ToFormularioEntity(this FormularioDTO dto)
    {
        var entity = new FormularioEntity
        {
            NivelExperiencia = dto.NivelExperiencia,
            ObjetivoCarreira = dto.ObjetivoCarreira,
            AreaTecnologia1 = dto.AreaTecnologia1,
            AreaTecnologia2 = dto.AreaTecnologia2,
            AreaTecnologia3 = dto.AreaTecnologia3,
            HorasEstudo = dto.HorasEstudo,
            Habilidades = dto.Habilidades,
            IdUsuario = dto.IdUsuario
        };

        entity.Id = default; // força o EF a tratar como novo registro

        return entity;
    }
}
