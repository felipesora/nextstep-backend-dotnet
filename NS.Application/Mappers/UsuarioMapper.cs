using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Mappers;

public static class UsuarioMapper
{
    public static UsuarioEntity ToUsuarioEntity(this UsuarioDTO dto)
    {
        return new UsuarioEntity
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha,
            DataCadastro = DateTime.Now
        };
    }
}
