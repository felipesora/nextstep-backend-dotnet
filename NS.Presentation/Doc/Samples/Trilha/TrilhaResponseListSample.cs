using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Trilha;

public class TrilhaResponseListSample : IExamplesProvider<IEnumerable<TrilhaEntity>>
{
    public IEnumerable<TrilhaEntity> GetExamples()
    {
        return new List<TrilhaEntity>
        {
            new TrilhaEntity
            {
                Id = 1,
                Nome = "Backend com Java e Spring Boot",
                Descricao = "Aprenda a desenvolver APIs REST robustas com Java e Spring Boot, integrando banco de dados, segurança e boas práticas de arquitetura.",
                Area = AreaTrilha.BACKEND,
                Nivel = NivelTrilha.INTERMEDIARIO,
                Status = StatusTrilha.ATIVA,
                DataCriacao = DateTime.Parse("12/11/2025 15:17:15")
            },
            new TrilhaEntity
            {
                Id = 2,
                Nome = "Frontend com React",
                Descricao = "Domine o desenvolvimento de interfaces modernas e performáticas com React, Hooks, Context API e integração com APIs REST.",
                Area = AreaTrilha.WEB,
                Nivel = NivelTrilha.AVANCADO,
                Status = StatusTrilha.ATIVA,
                DataCriacao = DateTime.Parse("15/11/2025 10:30:00")
            },
        };
    }
}
