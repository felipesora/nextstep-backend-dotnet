using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.NotaTrilha;

public class NotaTrilhaResponseSample : IExamplesProvider<NotaTrilhaEntity>
{
    public NotaTrilhaEntity GetExamples()
    {
        return new NotaTrilhaEntity()
        {
            Id = 1,
            ValorNota = 5,
            Observacao = "Trilha excelente! Explicações claras e exemplos práticos. Recomendo muito!",
            IdTrilha = 1,
            IdUsuario = 1
        };
    }
}
