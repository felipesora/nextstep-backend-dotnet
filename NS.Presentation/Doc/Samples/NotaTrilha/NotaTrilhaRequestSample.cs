using NS.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.NotaTrilha;

public class NotaTrilhaRequestSample : IExamplesProvider<NotaTrilhaDTO>
{
    public NotaTrilhaDTO GetExamples()
    {
        return new NotaTrilhaDTO
        {
            ValorNota = 5,
            Observacao = "Trilha excelente! Explicações claras e exemplos práticos. Recomendo muito!",
            IdTrilha = 1,
            IdUsuario = 1
        };
    }
}
