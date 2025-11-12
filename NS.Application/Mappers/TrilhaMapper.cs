using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class TrilhaMapper
{
    public static TrilhaEntity ToTrilhaEntity(this TrilhaDTO dto)
    {
        return new TrilhaEntity
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Area = dto.Area,
            Nivel = dto.Nivel,
            Status = dto.Status
        };
    }
}
