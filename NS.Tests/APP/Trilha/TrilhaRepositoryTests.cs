using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Infra.Data.AppData;
using NS.Infra.Data.Repositories;

namespace NS.Tests.APP.Trilha;

public class TrilhaRepositoryTests
{
    private static ApplicationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationContext(options);
    }

    private static TrilhaEntity BuildTrilha(
        string nome = "Trilha Backend",
        string descricao = "Descrição da trilha backend",
        AreaTrilha area = AreaTrilha.WEB,
        NivelTrilha nivel = NivelTrilha.INICIANTE,
        StatusTrilha status = StatusTrilha.ATIVA)
    {
        return new TrilhaEntity
        {
            Nome = nome,
            Descricao = descricao,
            Area = area,
            Nivel = nivel,
            Status = status,
            DataCriacao = DateTime.Now
        };
    }

    // ---------------------------------------------------------
    // READ - ObterTodasTrilhasAsync
    // ---------------------------------------------------------
    [Fact(DisplayName = "ObterTodasTrilhasAsync - Deve retornar lista paginada")]
    public async Task ObterTodasTrilhasAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetAllTrilhasDB");
        var repo = new TrilhaRepository(context);

        context.Trilha.AddRange(
            BuildTrilha("Trilha 1", "Descrição 1"),
            BuildTrilha("Trilha 2", "Descrição 2")
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTodasTrilhasAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    [Fact(DisplayName = "ObterTodasTrilhasAsync - Deve retornar lista vazia se não houver trilhas")]
    public async Task ObterTodasTrilhasAsync_DeveRetornarListaVazia()
    {
        using var context = CreateContext("GetAllTrilhasEmptyDB");
        var repo = new TrilhaRepository(context);

        var result = await repo.ObterTodasTrilhasAsync();

        Assert.NotNull(result);
        Assert.Equal(0, result.TotalRegistros);
        Assert.Empty(result.Data);
    }

    // ---------------------------------------------------------
    // READ - ObterTrilhasAtivasAsync
    // ---------------------------------------------------------
    [Fact(DisplayName = "ObterTrilhasAtivasAsync - Deve retornar apenas trilhas ativas")]
    public async Task ObterTrilhasAtivasAsync_DeveRetornarApenasAtivas()
    {
        using var context = CreateContext("GetActiveTrilhasDB");
        var repo = new TrilhaRepository(context);

        context.Trilha.AddRange(
            BuildTrilha("Ativa 1", "Desc", status: StatusTrilha.ATIVA),
            BuildTrilha("Ativa 2", "Desc", status: StatusTrilha.ATIVA),
            BuildTrilha("Inativa", "Desc", status: StatusTrilha.INATIVA)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTrilhasAtivasAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
        Assert.All(result.Data, t => Assert.Equal(StatusTrilha.ATIVA, t.Status));
    }

    [Fact(DisplayName = "ObterTrilhasAtivasAsync - Deve retornar lista vazia quando não houver trilhas ativas")]
    public async Task ObterTrilhasAtivasAsync_DeveRetornarVazio()
    {
        using var context = CreateContext("GetActiveTrilhasEmptyDB");
        var repo = new TrilhaRepository(context);

        context.Trilha.Add(BuildTrilha("Inativa", "Desc", status: StatusTrilha.INATIVA));
        await context.SaveChangesAsync();

        var result = await repo.ObterTrilhasAtivasAsync();

        Assert.NotNull(result);
        Assert.Equal(0, result.TotalRegistros);
        Assert.Empty(result.Data);
    }

    // ---------------------------------------------------------
    // READ - ObterTrilhaPorIdAsync
    // ---------------------------------------------------------
    [Fact(DisplayName = "ObterTrilhaPorIdAsync - Deve retornar trilha existente")]
    public async Task ObterTrilhaPorIdAsync_DeveRetornarTrilha()
    {
        using var context = CreateContext("GetTrilhaByIdDB");
        var repo = new TrilhaRepository(context);

        var trilha = BuildTrilha("Trilha X", "Descrição X");
        context.Trilha.Add(trilha);
        await context.SaveChangesAsync();

        var result = await repo.ObterTrilhaPorIdAsync(trilha.Id);

        Assert.NotNull(result);
        Assert.Equal("Trilha X", result.Nome);
    }

    [Fact(DisplayName = "ObterTrilhaPorIdAsync - Deve retornar null para ID inexistente")]
    public async Task ObterTrilhaPorIdAsync_DeveRetornarNull()
    {
        using var context = CreateContext("GetTrilhaByIdNullDB");
        var repo = new TrilhaRepository(context);

        var result = await repo.ObterTrilhaPorIdAsync(999);

        Assert.Null(result);
    }
}
