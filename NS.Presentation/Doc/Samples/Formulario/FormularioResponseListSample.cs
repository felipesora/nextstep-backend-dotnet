using NS.Domain.Entities;
using NS.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Doc.Samples.Formulario;

public class FormularioResponseListSample : IExamplesProvider<IEnumerable<FormularioEntity>>
{
    public IEnumerable<FormularioEntity> GetExamples()
    {
        return new List<FormularioEntity>
        {
            new FormularioEntity()
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
            },
            new FormularioEntity()
            {
                Id = 1,
                NivelExperiencia = NivelExperiencia.INTERMEDIARIO,
                ObjetivoCarreira = ObjetivoCarreira.PRIMEIRO_EMPREGO,
                AreaTecnologia1 = AreaTecnologia.BACKEND,
                AreaTecnologia2 = AreaTecnologia.CLOUD,
                AreaTecnologia3 = AreaTecnologia.FRONTEND,
                HorasEstudo = HorasEstudo.MAIS_DE_15H,
                Habilidades = "Java, Spring Boot, Docker, Git, SQL.",
                IdUsuario = 2
            },
        };
    }
}
