using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.NotaTrilha;

public class NotaTrilhaResponseListSample : IExamplesProvider<IEnumerable<NotaTrilhaEntity>>
{
    public IEnumerable<NotaTrilhaEntity> GetExamples()
    {
        return new List<NotaTrilhaEntity>
        {
            new NotaTrilhaEntity()
            {
                Id = 1,
                ValorNota = 5,
                Observacao = "Trilha excelente! Explicações claras e exemplos práticos. Recomendo muito!",
                IdTrilha = 1,
            },
            new NotaTrilhaEntity()
            {
                Id = 2,
                ValorNota = 3,
                Observacao = "Conteúdo bom, mas poderia ter mais exercícios práticos e desafios no final.",
                IdTrilha = 2,
            },
        };
    }
}
