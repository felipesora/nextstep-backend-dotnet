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

namespace NS.Tests.APP.Usuario;

// ========================================
// CUSTOM FACTORY PARA TESTES DO USUÁRIO
// ========================================
public class CustomWebApplicationFactoryUsuario : WebApplicationFactory<Program>
{
    public Mock<IUsuarioService> UsuarioServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Substitui o service real pelo mock
            services.RemoveAll(typeof(IUsuarioService));
            services.AddSingleton<IUsuarioService>(UsuarioServiceMock.Object);
        });
    }
}

// ========================================
// TESTES DO CONTROLLER USUÁRIO
// ========================================
public class UsuarioControllerTest : IClassFixture<CustomWebApplicationFactoryUsuario>
{
    private readonly CustomWebApplicationFactoryUsuario _factory;

    public UsuarioControllerTest(CustomWebApplicationFactoryUsuario factory)
    {
        _factory = factory;
    }

    // ========================================
    // GET
    // ========================================
    [Fact(DisplayName = "GET /api/usuario - Deve retornar lista de usuários")]
    [Trait("Controller", "Usuario")]
    public async Task Get_DeveRetornarListaDeUsuarios()
    {
        var usuarios = new List<UsuarioEntity>
        {
            new UsuarioEntity { Id = 1, Nome = "Felipe", Email = "felipe@email.com" },
            new UsuarioEntity { Id = 2, Nome = "Maria", Email = "maria@email.com" }
        };

        var page = new PageResultModel<IEnumerable<UsuarioEntity>>
        {
            Data = usuarios,
            Deslocamento = 0,
            RegistrosRetornados = 2,
            TotalRegistros = 2
        };

        var retorno = OperationResult<PageResultModel<IEnumerable<UsuarioEntity>>>.Success(page, (int)HttpStatusCode.OK);

        _factory.UsuarioServiceMock
            .Setup(s => s.ObterTodosUsuariosAsync(0, 10))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/usuario");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // GET by ID
    // ========================================
    [Fact(DisplayName = "GET /api/usuario/{id} - Deve retornar usuário pelo ID")]
    [Trait("Controller", "Usuario")]
    public async Task GetId_DeveRetornarUsuarioPorId()
    {
        var usuario = new UsuarioEntity
        {
            Id = 1,
            Nome = "Felipe",
            Email = "felipe@email.com"
        };

        var retorno = OperationResult<UsuarioEntity?>.Success(usuario, (int)HttpStatusCode.OK);

        _factory.UsuarioServiceMock
            .Setup(s => s.ObterUsuarioPorIdAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/usuario/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // POST
    // ========================================
    [Fact(DisplayName = "POST /api/usuario - Deve cadastrar novo usuário")]
    [Trait("Controller", "Usuario")]
    public async Task Post_DeveCadastrarUsuario()
    {
        var dto = new UsuarioDTO
        {
            Nome = "Felipe",
            Email = "felipe@email.com",
            Senha = "123456"
        };

        var entity = new UsuarioEntity
        {
            Id = 1,
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };

        var retorno = OperationResult<UsuarioEntity?>.Success(entity, (int)HttpStatusCode.OK);

        _factory.UsuarioServiceMock
            .Setup(s => s.AdicionarUsuarioAsync(It.IsAny<UsuarioDTO>()))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/usuario", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // PUT
    // ========================================
    [Fact(DisplayName = "PUT /api/usuario/{id} - Deve editar usuário com sucesso")]
    [Trait("Controller", "Usuario")]
    public async Task Put_DeveEditarUsuario()
    {
        var dto = new UsuarioDTO
        {
            Nome = "Nome Atualizado",
            Email = "email@teste.com",
            Senha = "novaSenha123"
        };

        var entity = new UsuarioEntity
        {
            Id = 1,
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };

        var retorno = OperationResult<UsuarioEntity?>.Success(entity, (int)HttpStatusCode.OK);

        _factory.UsuarioServiceMock
            .Setup(s => s.EditarUsuarioAsync(1, It.IsAny<UsuarioDTO>()))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.PutAsJsonAsync("/api/usuario/1", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // ========================================
    // DELETE
    // ========================================
    [Fact(DisplayName = "DELETE /api/usuario/{id} - Deve deletar usuário com sucesso")]
    [Trait("Controller", "Usuario")]
    public async Task Delete_DeveDeletarUsuario()
    {
        var retorno = OperationResult<UsuarioEntity?>.Success(null, (int)HttpStatusCode.OK);

        _factory.UsuarioServiceMock
            .Setup(s => s.DeletarUsuarioAsync(1))
            .ReturnsAsync(retorno);

        using var client = _factory.CreateClient();

        var response = await client.DeleteAsync("/api/usuario/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
