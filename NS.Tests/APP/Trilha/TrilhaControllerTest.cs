using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace NS.Tests.APP.Trilha;

// ========================================
// CUSTOM FACTORY PARA TESTES DA TRILHA
// ========================================
public class CustomWebApplicationFactoryTrilha : WebApplicationFactory<Program>
{
    public Mock<ITrilhaService> TrilhaServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(ITrilhaService));
            services.AddSingleton<ITrilhaService>(TrilhaServiceMock.Object);
        });
    }
}

// ========================================
// TESTES DO CONTROLLER TRILHA
// ========================================
public class TrilhaControllerTest : IClassFixture<CustomWebApplicationFactoryTrilha>
{
    private readonly CustomWebApplicationFactoryTrilha _factory;

    public TrilhaControllerTest(CustomWebApplicationFactoryTrilha factory)
    {
        _factory = factory;
    }

    // ========================================
    // GET ALL
    // ========================================
    [Fact(DisplayName = "GET /api/trilha - Deve retornar lista de trilhas")]
    [Trait("Controller", "Trilha")]
    public async Task GetAll_DeveRetornarListaDeTrilhas()
    {
        var trilhas = new List<TrilhaEntity>
        {
            new TrilhaEntity { Id = 1, Nome = "Backend", Descricao = "Java + APIs" },
            new TrilhaEntity { Id = 2, Nome = "Frontend", Descricao = "React + UI" }
        };

        var page = new PageResultModel<IEnumerable<TrilhaEntity>>
        {
            Data = trilhas,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };

        var retorno = OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.TrilhaServiceMock
            .Setup(s => s.ObterTodasTrilhasAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/trilha");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET ATIVAS
    // ========================================
    [Fact(DisplayName = "GET /api/trilha/ativas - Deve retornar trilhas ativas")]
    [Trait("Controller", "Trilha")]
    public async Task GetAllAtivas_DeveRetornarTrilhasAtivas()
    {
        var trilhas = new List<TrilhaEntity>
        {
            new TrilhaEntity { Id = 1, Nome = "Backend", Descricao = "Java + APIs" }
        };

        var page = new PageResultModel<IEnumerable<TrilhaEntity>>
        {
            Data = trilhas,
            Deslocamento = 0,
            RegistrosRetornados = 1,
            TotalRegistros = 1
        };

        var retorno = OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.TrilhaServiceMock
            .Setup(s => s.ObterTrilhasAtivasAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/trilha/ativas");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET BY ID
    // ========================================
    [Fact(DisplayName = "GET /api/trilha/{id} - Deve retornar trilha pelo ID")]
    [Trait("Controller", "Trilha")]
    public async Task GetId_DeveRetornarTrilhaPorId()
    {
        var trilha = new TrilhaEntity
        {
            Id = 1,
            Nome = "FullStack",
            Descricao = "Java + React"
        };

        var retorno = OperationResult<TrilhaEntity?>.Success(trilha, (int)HttpStatusCode.OK);

        _factory.TrilhaServiceMock
            .Setup(s => s.ObterTrilhaPorIdAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/trilha/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
