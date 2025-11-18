using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Infra.Data.AppData;
using NS.Infra.Data.Repositories;

namespace NS.Tests.APP.Conteudo;

public class ConteudoRepositoryTests
{
    private static ApplicationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationContext(options);
    }

    private static ConteudoEntity BuildConteudo(
        string titulo = "Conteudo Teste",
        string descricao = "Descrição do conteúdo para teste",
        TipoConteudo tipo = TipoConteudo.VIDEO,
        long idTrilha = 1,
        string? link = "https://teste.com")
    {
        return new ConteudoEntity
        {
            Titulo = titulo,
            Descricao = descricao,
            Tipo = tipo,
            IdTrilha = idTrilha,
            Link = link,
            DataCriacao = DateTime.UtcNow
        };
    }

    // ----------------------------------------
    // READ - ObterTodosConteudosAsync
    // ----------------------------------------
    [Fact(DisplayName = "ObterTodosConteudosAsync - Deve retornar lista paginada de conteúdos")]
    public async Task ObterTodosConteudosAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetAllConteudosDB");
        var repo = new ConteudoRepository(context);

        context.Conteudo.AddRange(
            BuildConteudo("Conteudo 1", "Descrição 1"),
            BuildConteudo("Conteudo 2", "Descrição 2")
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTodosConteudosAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    // ----------------------------------------
    // READ - ObterConteudosPorIdTrilhaAsync
    // ----------------------------------------
    [Fact(DisplayName = "ObterConteudosPorIdTrilhaAsync - Deve retornar conteúdos filtrados por trilha")]
    public async Task ObterConteudosPorIdTrilhaAsync_DeveRetornarListaFiltrada()
    {
        using var context = CreateContext("GetConteudosByTrilhaDB");
        var repo = new ConteudoRepository(context);

        context.Conteudo.AddRange(
            BuildConteudo("Conteudo 1", "Descrição 1", idTrilha: 1),
            BuildConteudo("Conteudo 2", "Descrição 2", idTrilha: 2)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterConteudosPorIdTrilhaAsync(1);

        Assert.NotNull(result);
        Assert.Single(result.Data);
        Assert.All(result.Data, c => Assert.Equal(1, c.IdTrilha));
    }

    // ----------------------------------------
    // READ - ObterConteudoPorIdAsync
    // ----------------------------------------
    [Fact(DisplayName = "ObterConteudoPorIdAsync - Deve retornar conteúdo pelo ID")]
    public async Task ObterConteudoPorIdAsync_DeveRetornarConteudo()
    {
        using var context = CreateContext("GetConteudoByIdDB");
        var repo = new ConteudoRepository(context);

        var conteudo = BuildConteudo();
        context.Conteudo.Add(conteudo);
        await context.SaveChangesAsync();

        var result = await repo.ObterConteudoPorIdAsync(conteudo.Id);

        Assert.NotNull(result);
        Assert.Equal(conteudo.Titulo, result.Titulo);
    }

    [Fact(DisplayName = "ObterConteudoPorIdAsync - Deve retornar null se ID não existir")]
    public async Task ObterConteudoPorIdAsync_DeveRetornarNullSeNaoExistir()
    {
        using var context = CreateContext("GetConteudoByIdNullDB");
        var repo = new ConteudoRepository(context);

        var result = await repo.ObterConteudoPorIdAsync(999);

        Assert.Null(result);
    }
}
