using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class ConteudoMapper
{
    public static ConteudoEntity ToTrilhaEntity(this ConteudoDTO dto)
    {
        return new ConteudoEntity
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Tipo = dto.Tipo,
            Link = dto.Link,
            IdTrilha = dto.IdTrilha
        };
    }
}
