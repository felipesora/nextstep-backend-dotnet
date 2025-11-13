using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class NotaTrilhaMapper
{
    public static NotaTrilhaEntity ToNotaEntity(this NotaTrilhaDTO dto)
    {
        var entity = new NotaTrilhaEntity
        {
            ValorNota = dto.ValorNota,
            Observacao = dto.Observacao,
            IdTrilha = dto.IdTrilha,
            IdUsuario = dto.IdUsuario
        };

        entity.Id = default; // força o EF a tratar como novo registro

        return entity;
    }
}
