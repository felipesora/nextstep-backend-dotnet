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

namespace NS.Tests.APP.Formulario;

// ========================================
// CUSTOM FACTORY PARA TESTES DO FORMULÁRIO
// ========================================
public class CustomWebApplicationFactoryFormulario : WebApplicationFactory<Program>
{
    public Mock<IFormularioService> FormularioServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IFormularioService));
            services.AddSingleton<IFormularioService>(FormularioServiceMock.Object);
        });
    }
}

// ========================================
// TESTES DO CONTROLLER FORMULÁRIO
// ========================================
public class FormularioControllerTest : IClassFixture<CustomWebApplicationFactoryFormulario>
{
    private readonly CustomWebApplicationFactoryFormulario _factory;

    public FormularioControllerTest(CustomWebApplicationFactoryFormulario factory)
    {
        _factory = factory;
    }

    private static FormularioEntity BuildFormulario(long id = 1, long idUsuario = 1)
    {
        return new FormularioEntity
        {
            Id = id,
            IdUsuario = idUsuario,
            NivelExperiencia = NS.Domain.Enums.NivelExperiencia.INICIANTE,
            ObjetivoCarreira = NS.Domain.Enums.ObjetivoCarreira.CRESCER_AREA,
            AreaTecnologia1 = NS.Domain.Enums.AreaTecnologia.BACKEND,
            HorasEstudo = NS.Domain.Enums.HorasEstudo.DE_6_A_10H,
            Habilidades = "C# e SQL"
        };
    }

    private static FormularioDTO BuildFormularioDTO(long idUsuario = 1)
    {
        return new FormularioDTO
        {
            IdUsuario = idUsuario,
            NivelExperiencia = NS.Domain.Enums.NivelExperiencia.INICIANTE,
            ObjetivoCarreira = NS.Domain.Enums.ObjetivoCarreira.CRESCER_AREA,
            AreaTecnologia1 = NS.Domain.Enums.AreaTecnologia.BACKEND,
            HorasEstudo = NS.Domain.Enums.HorasEstudo.DE_6_A_10H,
            Habilidades = "C# e SQL"
        };
    }

    // ========================================
    // GET ALL
    // ========================================
    [Fact(DisplayName = "GET /api/formulario - Deve retornar lista de formulários")]
    public async Task GetAll_DeveRetornarLista()
    {
        var formularios = new List<FormularioEntity> { BuildFormulario(), BuildFormulario(2) };
        var page = new PageResultModel<IEnumerable<FormularioEntity>>
        {
            Data = formularios,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };
        var retorno = OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.FormularioServiceMock
            .Setup(s => s.ObterTodosFormulariosAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/formulario");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET BY USUARIO
    // ========================================
    [Fact(DisplayName = "GET /api/formulario/usuario?idUsuario=1 - Deve retornar formulários de usuário")]
    public async Task GetByUsuario_DeveRetornarLista()
    {
        var formularios = new List<FormularioEntity> { BuildFormulario(), BuildFormulario(2) };
        var page = new PageResultModel<IEnumerable<FormularioEntity>> { Data = formularios, Deslocamento = 0, RegistrosRetornados = 2, TotalRegistros = 2 };
        var retorno = OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.FormularioServiceMock
            .Setup(s => s.ObterFormulariosPorIdUsuarioAsync(1, 0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/formulario/usuario?idUsuario=1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET BY ID
    // ========================================
    [Fact(DisplayName = "GET /api/formulario/{id} - Deve retornar formulário pelo ID")]
    public async Task GetId_DeveRetornarFormularioPorId()
    {
        var formulario = BuildFormulario();
        var retorno = OperationResult<FormularioEntity?>.Success(formulario, (int)HttpStatusCode.OK);

        _factory.FormularioServiceMock
            .Setup(s => s.ObterFormularioPorIdAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/formulario/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // POST
    // ========================================
    [Fact(DisplayName = "POST /api/formulario - Deve cadastrar novo formulário")]
    public async Task Post_DeveCadastrarFormulario()
    {
        var dto = BuildFormularioDTO();
        var entity = BuildFormulario();
        var retorno = OperationResult<FormularioEntity?>.Success(entity, (int)HttpStatusCode.OK);

        _factory.FormularioServiceMock
            .Setup(s => s.AdicionarFormularioAsync(It.IsAny<FormularioDTO>()))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/formulario", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
