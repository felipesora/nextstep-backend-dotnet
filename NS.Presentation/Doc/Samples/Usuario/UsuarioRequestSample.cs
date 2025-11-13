using NS.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Usuario;

public class UsuarioRequestSample : IExamplesProvider<UsuarioDTO>
{
    public UsuarioDTO GetExamples()
    {
        return new UsuarioDTO
        {
            Nome = "Felipe Sora",
            Email = "felipe@email.com",
            Senha = "felipe123"
        };
    }
}
