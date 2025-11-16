using NS.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;
using NS.Domain.Enums;

namespace NS.Presentation.Doc.Samples.Formulario;

public class FormularioRequestSample : IExamplesProvider<FormularioDTO>
{
    public FormularioDTO GetExamples()
    {
        return new FormularioDTO
        {
            NivelExperiencia = NivelExperiencia.INICIANTE,
            ObjetivoCarreira = ObjetivoCarreira.APRENDER,
            AreaTecnologia1 = AreaTecnologia.FRONTEND,
            AreaTecnologia2 = AreaTecnologia.DESIGN,
            AreaTecnologia3 = AreaTecnologia.MOBILE,
            HorasEstudo = HorasEstudo.ATE_5H,
            Habilidades = "Conheço um pouco de HTML e CSS, mas ainda estou aprendendo o básico.",
            IdUsuario = 1
        };
    }
}
