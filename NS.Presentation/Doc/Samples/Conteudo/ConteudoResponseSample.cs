using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Conteudo;

public class ConteudoResponseSample : IExamplesProvider<ConteudoEntity>
{
    public ConteudoEntity GetExamples()
    {
        return new ConteudoEntity()
        {
            Id = 1,
            Titulo = "Introdução ao Spring Boot",
            Descricao = "Aprenda os conceitos iniciais do Spring Boot, incluindo configuração, estrutura do projeto e dependências básicas.",
            Tipo = TipoConteudo.VIDEO,
            Link = "https://www.youtube.com/watch?v=9SGDpanrc8U",
            DataCriacao = DateTime.Parse("12/11/2025 15:17:15"),
            IdTrilha = 1
        };
    }
}
