using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Domain.Enums;
using System.Net;
using System.Net.Http.Json;

namespace NS.Tests.APP.Conteudo;

// ========================================
// CUSTOM FACTORY PARA TESTES DO CONTEÚDO
// ========================================
public class CustomWebApplicationFactoryConteudo : WebApplicationFactory<Program>
{
    public Mock<IConteudoService> ConteudoServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Substitui o service real pelo mock
            services.RemoveAll(typeof(IConteudoService));
            services.AddSingleton<IConteudoService>(ConteudoServiceMock.Object);
        });
    }
}

// ========================================
// TESTES DO CONTROLLER CONTEÚDO
// ========================================
public class ConteudoControllerTest : IClassFixture<CustomWebApplicationFactoryConteudo>
{
    private readonly CustomWebApplicationFactoryConteudo _factory;

    public ConteudoControllerTest(CustomWebApplicationFactoryConteudo factory)
    {
        _factory = factory;
    }

    private static ConteudoEntity BuildConteudo(
        long id = 1,
        string titulo = "Conteudo Teste",
        string descricao = "Descrição do conteúdo para teste",
        TipoConteudo tipo = TipoConteudo.VIDEO,
        long idTrilha = 1,
        string? link = "https://teste.com")
    {
        return new ConteudoEntity
        {
            Id = id,
            Titulo = titulo,
            Descricao = descricao,
            Tipo = tipo,
            IdTrilha = idTrilha,
            Link = link,
            DataCriacao = System.DateTime.UtcNow
        };
    }

    // ========================================
    // GET /api/conteudo
    // ========================================
    [Fact(DisplayName = "GET /api/conteudo - Deve retornar lista de conteúdos")]
    public async Task GetAll_DeveRetornarListaDeConteudos()
    {
        var conteudos = new List<ConteudoEntity>
            {
                BuildConteudo(),
                BuildConteudo(2, "Conteudo 2", "Descrição 2")
            };

        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = conteudos,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };

        var retorno = OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.ConteudoServiceMock
            .Setup(s => s.ObterTodosConteudosAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/conteudo");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET /api/conteudo/trilha?idTrilha=
    // ========================================
    [Fact(DisplayName = "GET /api/conteudo/trilha - Deve retornar conteúdos por trilha")]
    public async Task GetByTrilha_DeveRetornarConteudosPorTrilha()
    {
        var conteudos = new List<ConteudoEntity>
            {
                BuildConteudo(idTrilha: 1),
                BuildConteudo(2, "Conteudo 2", "Descrição 2", idTrilha: 1)
            };

        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = conteudos,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };

        var retorno = OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.ConteudoServiceMock
            .Setup(s => s.ObterConteudosPorIdTrilhaAsync(1, 0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/conteudo/trilha?idTrilha=1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET /api/conteudo/{id}
    // ========================================
    [Fact(DisplayName = "GET /api/conteudo/{id} - Deve retornar conteúdo pelo ID")]
    public async Task GetId_DeveRetornarConteudoPorId()
    {
        var conteudo = BuildConteudo();

        var retorno = OperationResult<ConteudoEntity?>.Success(conteudo, (int)HttpStatusCode.OK);

        _factory.ConteudoServiceMock
            .Setup(s => s.ObterConteudoPorIdAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/conteudo/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
