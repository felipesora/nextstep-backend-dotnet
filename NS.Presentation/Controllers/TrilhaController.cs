using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Presentation.Doc.Samples.Trilha;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrilhaController : ControllerBase
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ITrilhaService _trilhaService;

    public TrilhaController(ITrilhaService trilhaService)
    {
        _trilhaService = trilhaService;
    }

    #endregion

    #region :: READ
    [HttpGet]
    [SwaggerOperation(
            Summary = "Lista de todas as Trilhas",
            Description = "Retorna a lista completa de todas as trilhas cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<TrilhaEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(TrilhaResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetAll(int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _trilhaService.ObterTodasTrilhasAsync(deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.Nome,
                t.Descricao,
                t.Area,
                t.Nivel,
                t.Status,
                t.DataCriacao,
                t.Conteudos,
                t.Notas,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Trilha", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Trilha", null),
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
            Summary = "Lista de Trilhas Ativas",
            Description = "Retorna a lista de trilhas ativas cadastradas."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<TrilhaEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(TrilhaResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetAllAtivas(int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _trilhaService.ObterTrilhasAtivasAsync(deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.Nome,
                t.Descricao,
                t.Area,
                t.Nivel,
                t.Status,
                t.DataCriacao,
                t.Conteudos,
                t.Notas,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Trilha", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Trilha", null),
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
            Summary = "Obtém uma Trilha pelo ID",
            Description = "Retorna a trilha correspondente ao ID informado."
        )]
    [SwaggerResponse(statusCode: 200, description: "Trilha encontrada", type: typeof(TrilhaEntity))]
    [SwaggerResponse(statusCode: 404, description: "Trilha não encontrada")]
    [SwaggerResponseExample(statusCode: 200, typeof(TrilhaResponseSample))]
    public async Task<IActionResult> GetId(long id)
    {
        var trilha = await _trilhaService.ObterTrilhaPorIdAsync(id);

        if (!trilha.IsSuccess) return StatusCode(trilha.StatusCode, trilha.Error);

        var response = new
        {
            trilha.Value.Id,
            trilha.Value.Nome,
            trilha.Value.Descricao,
            trilha.Value.Area,
            trilha.Value.Nivel,
            trilha.Value.Status,
            trilha.Value.DataCriacao,
            trilha.Value.Conteudos,
            trilha.Value.Notas
        };

        return Ok(response);
    }

    #endregion
}
