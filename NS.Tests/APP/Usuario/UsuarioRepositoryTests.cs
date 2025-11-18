using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Infra.Data.AppData;
using NS.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.Tests.APP.Usuario;

public class UsuarioRepositoryTests
{
    private static ApplicationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationContext(options);
    }

    private static UsuarioEntity BuildUsuario(
        string nome = "Felipe Sora",
        string email = "felipe@email.com",
        string senha = "123456")
    {
        return new UsuarioEntity
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };
    }

    // ----------------------------------------
    // CREATE
    // ----------------------------------------
    [Fact(DisplayName = "AdicionarUsuarioAsync - Deve adicionar usuário com sucesso")]
    public async Task AdicionarUsuarioAsync_DeveAdicionarComSucesso()
    {
        using var context = CreateContext("AddUserDB");
        var repo = new UsuarioRepository(context);

        var usuario = BuildUsuario();

        var result = await repo.AdicionarUsuarioAsync(usuario);

        Assert.NotNull(result);
        Assert.Equal("Felipe Sora", result.Nome);
        Assert.Single(context.Usuario);
    }

    // ----------------------------------------
    // READ - GetAll
    // ----------------------------------------
    [Fact(DisplayName = "ObterTodosUsuariosAsync - Deve retornar lista paginada de usuários")]
    public async Task ObterTodosUsuariosAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetAllUserDB");
        var repo = new UsuarioRepository(context);

        context.Usuario.AddRange(
            BuildUsuario("User1", "u1@teste.com", "123"),
            BuildUsuario("User2", "u2@teste.com", "456")
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTodosUsuariosAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    // ----------------------------------------
    // READ - GetById
    // ----------------------------------------
    [Fact(DisplayName = "ObterUsuarioPorIdAsync - Deve retornar usuário por ID")]
    public async Task ObterUsuarioPorIdAsync_DeveRetornarUsuario()
    {
        using var context = CreateContext("GetUserByIdDB");
        var repo = new UsuarioRepository(context);

        var usuario = BuildUsuario("Felipe", "felipe@teste.com", "abc123");
        context.Usuario.Add(usuario);
        await context.SaveChangesAsync();

        var result = await repo.ObterUsuarioPorIdAsync(usuario.Id);

        Assert.NotNull(result);
        Assert.Equal("Felipe", result.Nome);
    }

    // ----------------------------------------
    // UPDATE
    // ----------------------------------------
    [Fact(DisplayName = "EditarUsuarioAsync - Deve atualizar usuário existente")]
    public async Task EditarUsuarioAsync_DeveAtualizarDados()
    {
        using var context = CreateContext("EditUserDB");
        var repo = new UsuarioRepository(context);

        var usuario = BuildUsuario("Antigo", "antigo@teste.com", "111");
        context.Usuario.Add(usuario);
        await context.SaveChangesAsync();

        var novo = BuildUsuario("Atualizado", "novo@teste.com", "999");

        var result = await repo.EditarUsuarioAsync(usuario.Id, novo);

        Assert.NotNull(result);
        Assert.Equal("Atualizado", result.Nome);
        Assert.Equal("novo@teste.com", result.Email);
        Assert.Equal("999", result.Senha);
    }

    [Fact(DisplayName = "EditarUsuarioAsync - Deve retornar null se usuário não existir")]
    public async Task EditarUsuarioAsync_DeveRetornarNullSeNaoExistir()
    {
        using var context = CreateContext("EditUserNullDB");
        var repo = new UsuarioRepository(context);

        var novo = BuildUsuario("Inexistente", "none@teste.com", "xxx");
        var result = await repo.EditarUsuarioAsync(999, novo);

        Assert.Null(result);
    }

    // ----------------------------------------
    // DELETE
    // ----------------------------------------
    [Fact(DisplayName = "DeletarUsuarioAsync - Deve remover usuário existente")]
    public async Task DeletarUsuarioAsync_DeveRemoverComSucesso()
    {
        using var context = CreateContext("DeleteUserDB");
        var repo = new UsuarioRepository(context);

        var usuario = BuildUsuario("User", "user@teste.com", "123");
        context.Usuario.Add(usuario);
        await context.SaveChangesAsync();

        var result = await repo.DeletarUsuarioAsync(usuario.Id);

        Assert.NotNull(result);
        Assert.Empty(context.Usuario);
    }

    [Fact(DisplayName = "DeletarUsuarioAsync - Deve retornar null se usuário não existir")]
    public async Task DeletarUsuarioAsync_DeveRetornarNullSeNaoExistir()
    {
        using var context = CreateContext("DeleteUserNullDB");
        var repo = new UsuarioRepository(context);

        var result = await repo.DeletarUsuarioAsync(999);

        Assert.Null(result);
    }

    // ----------------------------------------
    // VALIDATIONS
    // ----------------------------------------
    [Fact(DisplayName = "ExisteOutroComMesmoEmailAsync - Deve detectar email duplicado")]
    public async Task ExisteOutroComMesmoEmailAsync_DeveDetectarDuplicado()
    {
        using var context = CreateContext("EmailDupUserDB");
        var repo = new UsuarioRepository(context);

        var u1 = BuildUsuario("User1", "email@teste.com", "111");
        var u2 = BuildUsuario("User2", "outro@teste.com", "222");

        context.Usuario.AddRange(u1, u2);
        await context.SaveChangesAsync();

        var existe = await repo.ExisteOutroComMesmoEmailAsync(u2.Id, "email@teste.com");

        Assert.True(existe);
    }
}