using Moq;
using NS.Application.Services;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.Tests.APP.Conteudo;

public class ConteudoServiceTests
{
    private readonly Mock<IConteudoRepository> _conteudoRepositoryMock;
    private readonly ConteudoService _conteudoService;

    public ConteudoServiceTests()
    {
        _conteudoRepositoryMock = new Mock<IConteudoRepository>();
        _conteudoService = new ConteudoService(_conteudoRepositoryMock.Object);
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
            DataCriacao = DateTime.UtcNow
        };
    }

    // ========================================
    // READ - ObterTodosConteudosAsync
    // ========================================
    [Fact(DisplayName = "ObterTodosConteudosAsync - Deve retornar conteúdos com sucesso")]
    public async Task ObterTodosConteudosAsync_DeveRetornarConteudos()
    {
        var conteudos = new List<ConteudoEntity>
            {
                BuildConteudo(),
                BuildConteudo(2, "Conteudo 2", "Descrição 2")
            };

        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = conteudos,
            TotalRegistros = 2,
            Deslocamento = 0,
            RegistrosRetornados = 2
        };

        _conteudoRepositoryMock
            .Setup(r => r.ObterTodosConteudosAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _conteudoService.ObterTodosConteudosAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.TotalRegistros);
    }

    [Fact(DisplayName = "ObterTodosConteudosAsync - Deve falhar se lista estiver vazia")]
    public async Task ObterTodosConteudosAsync_DeveFalhar_ListaVazia()
    {
        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = new List<ConteudoEntity>(),
            TotalRegistros = 0
        };

        _conteudoRepositoryMock
            .Setup(r => r.ObterTodosConteudosAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _conteudoService.ObterTodosConteudosAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para conteudos", result.Error);
    }

    // ========================================
    // READ - ObterConteudosPorIdTrilhaAsync
    // ========================================
    [Fact(DisplayName = "ObterConteudosPorIdTrilhaAsync - Deve retornar conteúdos filtrados por trilha")]
    public async Task ObterConteudosPorIdTrilhaAsync_DeveRetornarConteudos()
    {
        var conteudos = new List<ConteudoEntity>
            {
                BuildConteudo(idTrilha: 1),
                BuildConteudo(2, "Conteudo 2", "Descrição 2", idTrilha: 1)
            };

        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = conteudos,
            TotalRegistros = 2
        };

        _conteudoRepositoryMock
            .Setup(r => r.ObterConteudosPorIdTrilhaAsync(1, 0, 10))
            .ReturnsAsync(page);

        var result = await _conteudoService.ObterConteudosPorIdTrilhaAsync(1);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.TotalRegistros);
        Assert.All(result.Value.Data, c => Assert.Equal(1, c.IdTrilha));
    }

    [Fact(DisplayName = "ObterConteudosPorIdTrilhaAsync - Deve falhar se lista estiver vazia")]
    public async Task ObterConteudosPorIdTrilhaAsync_DeveFalhar_ListaVazia()
    {
        var page = new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = new List<ConteudoEntity>()
        };

        _conteudoRepositoryMock
            .Setup(r => r.ObterConteudosPorIdTrilhaAsync(1, 0, 10))
            .ReturnsAsync(page);

        var result = await _conteudoService.ObterConteudosPorIdTrilhaAsync(1);

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para conteudos dessa trilha", result.Error);
    }

    // ========================================
    // READ - ObterConteudoPorIdAsync
    // ========================================
    [Fact(DisplayName = "ObterConteudoPorIdAsync - Deve retornar conteúdo existente")]
    public async Task ObterConteudoPorIdAsync_DeveRetornarConteudo()
    {
        var conteudo = BuildConteudo();

        _conteudoRepositoryMock
            .Setup(r => r.ObterConteudoPorIdAsync(conteudo.Id))
            .ReturnsAsync(conteudo);

        var result = await _conteudoService.ObterConteudoPorIdAsync(conteudo.Id);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(conteudo.Titulo, result.Value!.Titulo);
    }

    [Fact(DisplayName = "ObterConteudoPorIdAsync - Deve falhar se conteúdo não existir")]
    public async Task ObterConteudoPorIdAsync_DeveFalhar_ConteudoNaoExiste()
    {
        _conteudoRepositoryMock
            .Setup(r => r.ObterConteudoPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((ConteudoEntity?)null);

        var result = await _conteudoService.ObterConteudoPorIdAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal("Conteudo não encontrado", result.Error);
    }
}
