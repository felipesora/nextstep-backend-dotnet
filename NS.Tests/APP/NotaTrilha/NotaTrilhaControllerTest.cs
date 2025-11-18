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

namespace NS.Tests.APP.NotaTrilha;

// ========================================
// CUSTOM FACTORY PARA TESTES DE NOTA TRILHA
// ========================================
public class CustomWebApplicationFactoryNotaTrilha : WebApplicationFactory<Program>
{
    public Mock<INotaTrilhaService> NotaServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Substitui o service real pelo mock
            services.RemoveAll(typeof(INotaTrilhaService));
            services.AddSingleton<INotaTrilhaService>(NotaServiceMock.Object);
        });
    }
}

// ========================================
// TESTES DO CONTROLLER NOTA TRILHA
// ========================================
public class NotaTrilhaControllerTest : IClassFixture<CustomWebApplicationFactoryNotaTrilha>
{
    private readonly CustomWebApplicationFactoryNotaTrilha _factory;

    public NotaTrilhaControllerTest(CustomWebApplicationFactoryNotaTrilha factory)
    {
        _factory = factory;
    }

    private static NotaTrilhaEntity BuildNota(long id = 1, int valor = 4)
    {
        return new NotaTrilhaEntity
        {
            Id = id,
            ValorNota = valor,
            IdTrilha = 10,
            IdUsuario = 20,
            Observacao = "Observação teste"
        };
    }

    // ========================================
    // GET /api/NotaTrilha
    // ========================================
    [Fact(DisplayName = "GET /api/NotaTrilha - Deve retornar lista de notas")]
    public async Task GetAll_DeveRetornarListaDeNotas()
    {
        var notas = new List<NotaTrilhaEntity> { BuildNota(), BuildNota(2, 5) };
        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = notas,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };
        var retorno = OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.NotaServiceMock
            .Setup(s => s.ObterTodasNotasAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/NotaTrilha");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET /api/NotaTrilha/{id}
    // ========================================
    [Fact(DisplayName = "GET /api/NotaTrilha/{id} - Deve retornar nota por ID")]
    public async Task GetId_DeveRetornarNotaPorId()
    {
        var nota = BuildNota();
        var retorno = OperationResult<NotaTrilhaEntity?>.Success(nota, (int)HttpStatusCode.OK);

        _factory.NotaServiceMock
            .Setup(s => s.ObterNotaPorIdAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/NotaTrilha/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET /api/NotaTrilha/ativas?idTrilha={id}
    // ========================================
    [Fact(DisplayName = "GET /api/NotaTrilha/ativas?idTrilha=10 - Deve retornar notas da trilha")]
    public async Task GetByTrilha_DeveRetornarNotasDaTrilha()
    {
        var notas = new List<NotaTrilhaEntity> { BuildNota() };
        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = notas,
            Deslocamento = 0,
            RegistrosRetornados = 1,
            TotalRegistros = 1
        };
        var retorno = OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.NotaServiceMock
            .Setup(s => s.ObterNotasPorIdTrilhaAsync(10, 0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/NotaTrilha/ativas?idTrilha=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // POST /api/NotaTrilha
    // ========================================
    [Fact(DisplayName = "POST /api/NotaTrilha - Deve cadastrar nova nota")]
    public async Task Post_DeveCadastrarNota()
    {
        var dto = new NotaTrilhaDTO
        {
            ValorNota = 5,
            Observacao = "Muito bom",
            IdTrilha = 10,
            IdUsuario = 20
        };

        var nota = BuildNota(1, 5);
        var retorno = OperationResult<NotaTrilhaEntity?>.Success(nota, (int)HttpStatusCode.OK);

        _factory.NotaServiceMock
            .Setup(s => s.AdicionarNotaAsync(It.IsAny<NotaTrilhaDTO>()))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/NotaTrilha", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // PUT /api/NotaTrilha/{id}
    // ========================================
    [Fact(DisplayName = "PUT /api/NotaTrilha/{id} - Deve atualizar nota existente")]
    public async Task Put_DeveAtualizarNota()
    {
        var dto = new NotaTrilhaDTO
        {
            ValorNota = 3,
            Observacao = "Atualizada",
            IdTrilha = 10,
            IdUsuario = 20
        };

        var nota = BuildNota(1, 3);
        var retorno = OperationResult<NotaTrilhaEntity?>.Success(nota, (int)HttpStatusCode.OK);

        _factory.NotaServiceMock
            .Setup(s => s.EditarNotaAsync(1, It.IsAny<NotaTrilhaDTO>()))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();
        var response = await client.PutAsJsonAsync("/api/NotaTrilha/1", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
