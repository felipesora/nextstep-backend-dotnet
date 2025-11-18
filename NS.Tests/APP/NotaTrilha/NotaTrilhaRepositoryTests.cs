using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Infra.Data.AppData;
using NS.Infra.Data.Repositories;

namespace NS.Tests.APP.NotaTrilha;

public class NotaTrilhaRepositoryTests
{
    private static ApplicationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationContext(options);
    }

    private static NotaTrilhaEntity BuildNota(
        int valor = 4,
        string obs = "Boa nota",
        long idTrilha = 1,
        long idUsuario = 1)
    {
        return new NotaTrilhaEntity
        {
            ValorNota = valor,
            Observacao = obs,
            IdTrilha = idTrilha,
            IdUsuario = idUsuario
        };
    }

    // ----------------------------------------
    // CREATE
    // ----------------------------------------
    [Fact(DisplayName = "AdicionarNotaAsync - Deve adicionar nota com sucesso")]
    public async Task AdicionarNotaAsync_DeveAdicionarComSucesso()
    {
        using var context = CreateContext("AddNotaDB");
        var repo = new NotaTrilhaRepository(context);

        var nota = BuildNota();

        var result = await repo.AdicionarNotaAsync(nota);

        Assert.NotNull(result);
        Assert.Equal(4, result.ValorNota);
        Assert.Single(context.Nota);
    }

    // ----------------------------------------
    // READ - GetAll
    // ----------------------------------------
    [Fact(DisplayName = "ObterTodasNotasAsync - Deve retornar lista paginada de notas")]
    public async Task ObterTodasNotasAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetAllNotasDB");
        var repo = new NotaTrilhaRepository(context);

        context.Nota.AddRange(
            BuildNota(3, "ok", 1, 1),
            BuildNota(5, "excelente", 1, 2)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTodasNotasAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    // ----------------------------------------
    // READ - GetById
    // ----------------------------------------
    [Fact(DisplayName = "ObterNotaPorIdAsync - Deve retornar nota por ID")]
    public async Task ObterNotaPorIdAsync_DeveRetornarNota()
    {
        using var context = CreateContext("GetNotaByIdDB");
        var repo = new NotaTrilhaRepository(context);

        var nota = BuildNota(5, "Top", 10, 1);
        context.Nota.Add(nota);
        await context.SaveChangesAsync();

        var result = await repo.ObterNotaPorIdAsync(nota.Id);

        Assert.NotNull(result);
        Assert.Equal(5, result.ValorNota);
    }

    // ----------------------------------------
    // READ - GetByTrilha
    // ----------------------------------------
    [Fact(DisplayName = "ObterNotasPorIdTrilhaAsync - Deve retornar notas filtradas por trilha")]
    public async Task ObterNotasPorIdTrilhaAsync_DeveRetornarListaFiltrada()
    {
        using var context = CreateContext("GetNotasByTrilhaDB");
        var repo = new NotaTrilhaRepository(context);

        context.Nota.AddRange(
            BuildNota(3, "ok", idTrilha: 1, idUsuario: 1),
            BuildNota(4, "bom", idTrilha: 1, idUsuario: 2),
            BuildNota(5, "ótimo", idTrilha: 2, idUsuario: 1)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterNotasPorIdTrilhaAsync(1);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    // ----------------------------------------
    // UPDATE
    // ----------------------------------------
    [Fact(DisplayName = "EditarNotaAsync - Deve atualizar nota existente")]
    public async Task EditarNotaAsync_DeveAtualizarComSucesso()
    {
        using var context = CreateContext("EditNotaDB");
        var repo = new NotaTrilhaRepository(context);

        var nota = BuildNota(2, "ruim", idTrilha: 1, idUsuario: 1);
        context.Nota.Add(nota);
        await context.SaveChangesAsync();

        var nova = BuildNota(5, "melhorou", idTrilha: 2, idUsuario: 1);

        var result = await repo.EditarNotaAsync(nota.Id, nova);

        Assert.NotNull(result);
        Assert.Equal(5, result.ValorNota);
        Assert.Equal("melhorou", result.Observacao);
        Assert.Equal(2, result.IdTrilha);
    }

    [Fact(DisplayName = "EditarNotaAsync - Deve retornar null se nota não existir")]
    public async Task EditarNotaAsync_DeveRetornarNullSeNaoExistir()
    {
        using var context = CreateContext("EditNotaNullDB");
        var repo = new NotaTrilhaRepository(context);

        var nova = BuildNota(5, "não existe", 1, 1);

        var result = await repo.EditarNotaAsync(999, nova);

        Assert.Null(result);
    }
}
