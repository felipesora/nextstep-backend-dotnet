using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Formulario;

public class FormularioResponseSample : IExamplesProvider<FormularioEntity>
{
    public FormularioEntity GetExamples()
    {
        return new FormularioEntity()
        {
            Id = 1,
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
