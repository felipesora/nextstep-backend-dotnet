using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class NotaTrilhaMapper
{
    public static NotaTrilhaEntity ToNotaEntity(this NotaTrilhaDTO dto)
    {
        return new NotaTrilhaEntity
        {
            ValorNota = dto.ValorNota,
            Observacao = dto.Observacao,
            IdTrilha = dto.IdTrilha
        };
    }
}
