using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Presentation.Doc.Samples.Formulario;
using NS.Presentation.Doc.Samples.NotaTrilha;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormularioController : ControllerBase
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly IFormularioService _formularioService;

    public FormularioController(IFormularioService formularioService)
    {
        _formularioService = formularioService;
    }

    #endregion

    #region :: READ
    [HttpGet]
    [SwaggerOperation(
            Summary = "Lista de todas as Respostas do Formulario",
            Description = "Retorna a lista completa de todas as respostas do formulario cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<FormularioEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(FormularioResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetAll(int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _formularioService.ObterTodosFormulariosAsync(deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.NivelExperiencia,
                t.ObjetivoCarreira,
                t.AreaTecnologia1,
                t.AreaTecnologia2,
                t.AreaTecnologia3,
                t.HorasEstudo,
                t.Habilidades,
                t.IdUsuario,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Formulario", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Formulario", null),
            },
            pagina = new
            {
                result.Value.Deslocamento,
                result.Value.RegistrosRetornados,
                result.Value.TotalRegistros
            }
        };

        return Ok(hateaos);
    }

    [HttpGet("usuario")]
    [SwaggerOperation(
            Summary = "Lista de Respostas do Formulario de um Usuário",
            Description = "Retorna a lista de Respostas do Formulario de um Usuário cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<FormularioEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(FormularioResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetByUsuario(long idUsuario, int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _formularioService.ObterFormulariosPorIdUsuarioAsync(idUsuario, deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.NivelExperiencia,
                t.ObjetivoCarreira,
                t.AreaTecnologia1,
                t.AreaTecnologia2,
                t.AreaTecnologia3,
                t.HorasEstudo,
                t.Habilidades,
                t.IdUsuario,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Formulario", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Formulario", null),
            },
            pagina = new
            {
                result.Value.Deslocamento,
                result.Value.RegistrosRetornados,
                result.Value.TotalRegistros
            }
        };

        return Ok(hateaos);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
            Summary = "Obtém uma Resposta do Formulario pelo ID",
            Description = "Retorna a Resposta do Formulario correspondente ao ID informado."
        )]
    [SwaggerResponse(statusCode: 200, description: "Resposta do Formulario encontrada", type: typeof(FormularioEntity))]
    [SwaggerResponse(statusCode: 404, description: "Resposta do Formulario não encontrada")]
    [SwaggerResponseExample(statusCode: 200, typeof(FormularioResponseSample))]
    public async Task<IActionResult> GetId(long id)
    {
        var formulario = await _formularioService.ObterFormularioPorIdAsync(id);

        if (!formulario.IsSuccess) return StatusCode(formulario.StatusCode, formulario.Error);

        var response = new
        {
            formulario.Value.Id,
            formulario.Value.NivelExperiencia,
            formulario.Value.ObjetivoCarreira,
            formulario.Value.AreaTecnologia1,
            formulario.Value.AreaTecnologia2,
            formulario.Value.AreaTecnologia3,
            formulario.Value.HorasEstudo,
            formulario.Value.Habilidades,
            formulario.Value.IdUsuario
        };

        return Ok(response);
    }

    #endregion

    #region :: CREATE

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cadastra uma nova Resposta do Formulario",
        Description = "Cadastra uma nova Resposta do Formulario no sistema e retorna os dados cadastrados."
    )]
    [SwaggerRequestExample(typeof(FormularioDTO), typeof(FormularioRequestSample))]
    [SwaggerResponse(statusCode: 200, description: "Resposta do Formulario salva com sucesso", type: typeof(FormularioEntity))]
    [SwaggerResponseExample(statusCode: 200, typeof(FormularioResponseSample))]
    public async Task<IActionResult> Post(FormularioDTO dto)
    {
        var result = await _formularioService.AdicionarFormularioAsync(dto);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        return StatusCode(result.StatusCode, result);
    }

    #endregion
}
