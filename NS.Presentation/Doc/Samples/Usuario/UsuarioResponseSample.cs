using NS.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Usuario;

public class UsuarioResponseSample : IExamplesProvider<UsuarioEntity>
{
    public UsuarioEntity GetExamples()
    {
        return new UsuarioEntity
        {
            Id = 1,
            Nome = "Felipe Sora",
            Email = "felipe@email.com",
            Senha = "felipe123",
            DataCadastro = DateTime.Now
        };
    }
}
