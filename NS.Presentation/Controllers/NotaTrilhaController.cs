using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Presentation.Doc.Samples.NotaTrilha;
using NS.Presentation.Doc.Samples.Trilha;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotaTrilhaController : ControllerBase
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly INotaTrilhaService _notaTrilhaService;

    public NotaTrilhaController(INotaTrilhaService notaTrilhaService)
    {
        _notaTrilhaService = notaTrilhaService;
    }

    #endregion

    #region :: READ
    [HttpGet]
    [SwaggerOperation(
            Summary = "Lista de todas as Notas",
            Description = "Retorna a lista completa de todas as notas cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<NotaTrilhaEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(NotaTrilhaResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetAll(int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _notaTrilhaService.ObterTodasNotasAsync(deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.ValorNota,
                t.Observacao,
                t.IdTrilha,
                t.IdUsuario,
                links = new
                {
                    self = Url.Action(nameof(GetId), "NotaTrilha", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "NotaTrilha", null),
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

    [HttpGet("ativas")]
    [SwaggerOperation(
            Summary = "Lista de Notas de uma Trilha",
            Description = "Retorna a lista de notas de uma trilha cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<NotaTrilhaEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(NotaTrilhaResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetByTrilha(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _notaTrilhaService.ObterNotasPorIdTrilhaAsync(idTrilha, deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.ValorNota,
                t.Observacao,
                t.IdTrilha,
                t.IdUsuario,
                links = new
                {
                    self = Url.Action(nameof(GetId), "NotaTrilha", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "NotaTrilha", null),
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
            Summary = "Obtém uma Nota pelo ID",
            Description = "Retorna a nota correspondente ao ID informado."
        )]
    [SwaggerResponse(statusCode: 200, description: "Nota encontrada", type: typeof(NotaTrilhaEntity))]
    [SwaggerResponse(statusCode: 404, description: "Nota não encontrada")]
    [SwaggerResponseExample(statusCode: 200, typeof(NotaTrilhaResponseSample))]
    public async Task<IActionResult> GetId(long id)
    {
        var trilha = await _notaTrilhaService.ObterNotaPorIdAsync(id);

        if (!trilha.IsSuccess) return StatusCode(trilha.StatusCode, trilha.Error);

        var response = new
        {
            trilha.Value.Id,
            trilha.Value.ValorNota,
            trilha.Value.Observacao,
            trilha.Value.IdTrilha,
            trilha.Value.IdUsuario
        };

        return Ok(response);
    }

    #endregion

    #region :: CREATE

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cadastra uma nova nota",
        Description = "Cadastra uma nova nota no sistema e retorna os dados cadastrados."
    )]
    [SwaggerRequestExample(typeof(NotaTrilhaDTO), typeof(NotaTrilhaRequestSample))]
    [SwaggerResponse(statusCode: 200, description: "Nota salva com sucesso", type: typeof(NotaTrilhaEntity))]
    [SwaggerResponseExample(statusCode: 200, typeof(NotaTrilhaResponseSample))]
    public async Task<IActionResult> Post(NotaTrilhaDTO dto)
    {
        var result = await _notaTrilhaService.AdicionarNotaAsync(dto);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        return StatusCode(result.StatusCode, result);
    }

    #endregion

    #region :: UPDATE

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Atualiza uma nota",
        Description = "Edita os dados de uma nota já cadastrada com base no ID informado."
    )]
    [SwaggerResponse(statusCode: 200, description: "Nota atualizada com sucesso", type: typeof(NotaTrilhaEntity))]
    [SwaggerResponse(statusCode: 400, description: "Erro na requisição (validação ou dados inválidos)")]
    [SwaggerResponse(statusCode: 404, description: "Nota não encontrada")]
    [SwaggerRequestExample(typeof(NotaTrilhaDTO), typeof(NotaTrilhaRequestSample))]
    [SwaggerResponseExample(statusCode: 200, typeof(NotaTrilhaResponseSample))]
    public async Task<IActionResult> Put(long id, NotaTrilhaDTO dto)
    {
        var result = await _notaTrilhaService.EditarNotaAsync(id, dto);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        return StatusCode(result.StatusCode, result);
    }

    #endregion
}
