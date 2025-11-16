using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Presentation.Doc.Samples.Conteudo;
using NS.Presentation.Doc.Samples.Trilha;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace NS.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConteudoController : ControllerBase
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly IConteudoService _conteudoService;

    public ConteudoController(IConteudoService conteudoService)
    {
        _conteudoService = conteudoService;
    }

    #endregion

    #region :: READ
    [HttpGet]
    [SwaggerOperation(
            Summary = "Lista de todos os Conteúdos",
            Description = "Retorna a lista completa detodos os conteúdos cadastrados."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<ConteudoEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(ConteudoResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetAll(int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _conteudoService.ObterTodosConteudosAsync(deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.Titulo,
                t.Descricao,
                t.Tipo,
                t.Link,
                t.IdTrilha,
                t.DataCriacao,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Conteudo", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Conteudo", null),
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

    [HttpGet("trilha")]
    [SwaggerOperation(
            Summary = "Lista de Conteúdos de uma Trilha",
            Description = "Retorna a lista de conteúdos cadastrados de uma trilha."
        )]
    [SwaggerResponse(statusCode: 200, description: "Lista retornada com sucesso", type: typeof(IEnumerable<ConteudoEntity>))]
    [SwaggerResponse(statusCode: 204, description: "Lista não tem dados")]
    [SwaggerResponseExample(statusCode: 200, typeof(ConteudoResponseListSample))]
    [EnableRateLimiting("NextStep")]
    public async Task<IActionResult> GetByTrilha(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        var result = await _conteudoService.ObterConteudosPorIdTrilhaAsync(idTrilha, deslocamento, registrosRetornados);

        if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

        var hateaos = new
        {
            data = result.Value.Data.Select(t => new {
                t.Id,
                t.Titulo,
                t.Descricao,
                t.Tipo,
                t.Link,
                t.IdTrilha,
                t.DataCriacao,
                links = new
                {
                    self = Url.Action(nameof(GetId), "Conteudo", new { id = t.Id }, Request.Scheme),
                }
            }),
            links = new
            {
                self = Url.Action(nameof(GetId), "Conteudo", null),
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
            Summary = "Obtém um Conteúdo pelo ID",
            Description = "Retorna o conteúdo correspondente ao ID informado."
        )]
    [SwaggerResponse(statusCode: 200, description: "Conteúdo encontrado", type: typeof(ConteudoEntity))]
    [SwaggerResponse(statusCode: 404, description: "Conteúdo não encontrado")]
    [SwaggerResponseExample(statusCode: 200, typeof(ConteudoResponseSample))]
    public async Task<IActionResult> GetId(long id)
    {
        var conteudo = await _conteudoService.ObterConteudoPorIdAsync(id);

        if (!conteudo.IsSuccess) return StatusCode(conteudo.StatusCode, conteudo.Error);

        var response = new
        {
            conteudo.Value.Id,
            conteudo.Value.Titulo,
            conteudo.Value.Descricao,
            conteudo.Value.Tipo,
            conteudo.Value.Link,
            conteudo.Value.IdTrilha,
            conteudo.Value.DataCriacao,
        };

        return Ok(response);
    }

    #endregion
}
