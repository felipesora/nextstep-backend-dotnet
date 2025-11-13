using NS.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Usuario;

public class UsuarioResponseListSample : IExamplesProvider<IEnumerable<UsuarioEntity>>
{
    public IEnumerable<UsuarioEntity> GetExamples()
    {
        return new List<UsuarioEntity>
        {
            new UsuarioEntity
            {
                Id = 1,
                Nome = "Felipe Sora",
                Email = "felipe@email.com",
                Senha = "felipe123",
                DataCadastro = DateTime.Now
            },
            new UsuarioEntity
            {
                Id = 1,
                Nome = "Maria Silva",
                Email = "maria@email.com",
                Senha = "maria123",
                DataCadastro = DateTime.Now
            },
        };
    }
}
