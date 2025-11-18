using Moq;
using NS.Application.Services;
using NS.Domain.Entities;
using NS.Domain.Interfaces;

namespace NS.Tests.APP.Trilha;

public class TrilhaServiceTests
{
    private readonly Mock<ITrilhaRepository> _trilhaRepositoryMock;
    private readonly TrilhaService _trilhaService;

    public TrilhaServiceTests()
    {
        _trilhaRepositoryMock = new Mock<ITrilhaRepository>();
        _trilhaService = new TrilhaService(_trilhaRepositoryMock.Object);
    }

    private static TrilhaEntity BuildTrilha(long id = 1, string nome = "Trilha 1")
    {
        return new TrilhaEntity
        {
            Id = id,
            Nome = nome,
            Descricao = "Descrição válida da trilha",
            Area = NS.Domain.Enums.AreaTrilha.BACKEND,
            Nivel = NS.Domain.Enums.NivelTrilha.INICIANTE,
            Status = NS.Domain.Enums.StatusTrilha.ATIVA,
            DataCriacao = DateTime.Now
        };
    }

    private static PageResultModel<IEnumerable<TrilhaEntity>> BuildPage(IEnumerable<TrilhaEntity> trilhas)
    {
        var list = trilhas.ToList();

        return new PageResultModel<IEnumerable<TrilhaEntity>>
        {
            Data = list,
            TotalRegistros = list.Count,
            RegistrosRetornados = list.Count,
            Deslocamento = 0
        };
    }

    // ======================================================
    // READ - ObterTodasTrilhasAsync
    // ======================================================

    [Fact(DisplayName = "ObterTodasTrilhasAsync - Deve retornar trilhas corretamente")]
    public async Task ObterTodasTrilhasAsync_DeveRetornarTrilhas()
    {
        var trilhas = new List<TrilhaEntity> { BuildTrilha(), BuildTrilha(2, "Trilha 2") };

        _trilhaRepositoryMock
            .Setup(r => r.ObterTodasTrilhasAsync(0, 10))
            .ReturnsAsync(BuildPage(trilhas));

        var result = await _trilhaService.ObterTodasTrilhasAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.TotalRegistros);
    }

    [Fact(DisplayName = "ObterTodasTrilhasAsync - Deve falhar se não houver trilhas")]
    public async Task ObterTodasTrilhasAsync_DeveFalhar_ListaVazia()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTodasTrilhasAsync(0, 10))
            .ReturnsAsync(BuildPage(new List<TrilhaEntity>()));

        var result = await _trilhaService.ObterTodasTrilhasAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para trilhas", result.Error);
    }

    [Fact(DisplayName = "ObterTodasTrilhasAsync - Deve falhar ao lançar exceção")]
    public async Task ObterTodasTrilhasAsync_DeveFalhar_Excecao()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTodasTrilhasAsync(0, 10))
            .ThrowsAsync(new Exception());

        var result = await _trilhaService.ObterTodasTrilhasAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Ocorreu um erro ao buscar as trilhas", result.Error);
    }

    // ======================================================
    // READ - ObterTrilhasAtivasAsync
    // ======================================================

    [Fact(DisplayName = "ObterTrilhasAtivasAsync - Deve retornar trilhas ativas")]
    public async Task ObterTrilhasAtivasAsync_DeveRetornarTrilhasAtivas()
    {
        var trilhas = new List<TrilhaEntity> { BuildTrilha() };

        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhasAtivasAsync(0, 10))
            .ReturnsAsync(BuildPage(trilhas));

        var result = await _trilhaService.ObterTrilhasAtivasAsync();

        Assert.True(result.IsSuccess);
        Assert.Single(result.Value!.Data);
    }

    [Fact(DisplayName = "ObterTrilhasAtivasAsync - Deve falhar se lista vazia")]
    public async Task ObterTrilhasAtivasAsync_DeveFalhar_ListaVazia()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhasAtivasAsync(0, 10))
            .ReturnsAsync(BuildPage(new List<TrilhaEntity>()));

        var result = await _trilhaService.ObterTrilhasAtivasAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para trilhas ativas", result.Error);
    }

    [Fact(DisplayName = "ObterTrilhasAtivasAsync - Deve falhar com exceção")]
    public async Task ObterTrilhasAtivasAsync_DeveFalhar_Excecao()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhasAtivasAsync(0, 10))
            .ThrowsAsync(new Exception());

        var result = await _trilhaService.ObterTrilhasAtivasAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Ocorreu um erro ao buscar as trilhas ativas", result.Error);
    }

    // ======================================================
    // READ - ObterTrilhaPorIdAsync
    // ======================================================

    [Fact(DisplayName = "ObterTrilhaPorIdAsync - Deve retornar trilha existente")]
    public async Task ObterTrilhaPorIdAsync_DeveRetornarTrilha()
    {
        var trilha = BuildTrilha();

        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhaPorIdAsync(trilha.Id))
            .ReturnsAsync(trilha);

        var result = await _trilhaService.ObterTrilhaPorIdAsync(trilha.Id);

        Assert.True(result.IsSuccess);
        Assert.Equal(trilha.Nome, result.Value!.Nome);
    }

    [Fact(DisplayName = "ObterTrilhaPorIdAsync - Deve falhar se trilha não existir")]
    public async Task ObterTrilhaPorIdAsync_DeveFalhar_TrilhaNaoExiste()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhaPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((TrilhaEntity?)null);

        var result = await _trilhaService.ObterTrilhaPorIdAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal("Trilha não encontrada", result.Error);
    }

    [Fact(DisplayName = "ObterTrilhaPorIdAsync - Deve falhar com exceção")]
    public async Task ObterTrilhaPorIdAsync_DeveFalhar_Excecao()
    {
        _trilhaRepositoryMock
            .Setup(r => r.ObterTrilhaPorIdAsync(It.IsAny<long>()))
            .ThrowsAsync(new Exception());

        var result = await _trilhaService.ObterTrilhaPorIdAsync(1);

        Assert.False(result.IsSuccess);
        Assert.Equal("Ocorreu um erro ao buscar a trilha", result.Error);
    }
}
