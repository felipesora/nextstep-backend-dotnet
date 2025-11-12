using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Trilha;

public class TrilhaResponseSample : IExamplesProvider<TrilhaEntity>
{
    public TrilhaEntity GetExamples()
    {
        return new TrilhaEntity()
        {
            Id = 1,
            Nome = "Backend com Java e Spring Boot",
            Descricao = "Aprenda a desenvolver APIs REST robustas com Java e Spring Boot, integrando banco de dados, segurança e boas práticas de arquitetura",
            Area = AreaTrilha.BACKEND,
            Nivel = NivelTrilha.INTERMEDIARIO,
            Status = StatusTrilha.ATIVA,
            DataCriacao = DateTime.Parse("12/11/2025 15:17:15")
        };
    }
}
