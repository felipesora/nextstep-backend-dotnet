using Moq;
using NS.Application.Dtos;
using NS.Application.Services;
using NS.Domain.Entities;
using NS.Domain.Interfaces;

namespace NS.Tests.APP.NotaTrilha;

public class NotaTrilhaServiceTests
{
    private readonly Mock<INotaTrilhaRepository> _notaRepositoryMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<ITrilhaRepository> _trilhaRepositoryMock;
    private readonly NotaTrilhaService _service;

    public NotaTrilhaServiceTests()
    {
        _notaRepositoryMock = new Mock<INotaTrilhaRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _trilhaRepositoryMock = new Mock<ITrilhaRepository>();

        _service = new NotaTrilhaService(
            _notaRepositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _trilhaRepositoryMock.Object
        );
    }

    private static NotaTrilhaEntity BuildNota(
        long id = 1,
        int valor = 4,
        long idTrilha = 10,
        long idUsuario = 20)
    {
        return new NotaTrilhaEntity
        {
            Id = id,
            ValorNota = valor,
            IdTrilha = idTrilha,
            IdUsuario = idUsuario,
            Observacao = "Boa nota"
        };
    }

    private static TrilhaEntity BuildTrilha(long id = 10)
    {
        return new TrilhaEntity { Id = id, Nome = "Trilha Teste" };
    }

    private static UsuarioEntity BuildUsuario(long id = 20)
    {
        return new UsuarioEntity { Id = id, Nome = "Felipe", Email = "teste@email.com" };
    }

    // ============================================================
    // READ - ObterTodasNotas
    // ============================================================

    [Fact(DisplayName = "ObterTodasNotasAsync - Deve retornar notas com sucesso")]
    public async Task ObterTodasNotasAsync_DeveRetornarComSucesso()
    {
        var notas = new List<NotaTrilhaEntity> { BuildNota() };

        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = notas,
            TotalRegistros = 1,
            Deslocamento = 0,
            RegistrosRetornados = 1
        };

        _notaRepositoryMock
            .Setup(r => r.ObterTodasNotasAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _service.ObterTodasNotasAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value!.TotalRegistros);
    }

    [Fact(DisplayName = "ObterTodasNotasAsync - Deve falhar se lista estiver vazia")]
    public async Task ObterTodasNotasAsync_DeveFalhar_ListaVazia()
    {
        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = new List<NotaTrilhaEntity>(),
            TotalRegistros = 0
        };

        _notaRepositoryMock
            .Setup(r => r.ObterTodasNotasAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _service.ObterTodasNotasAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para notas", result.Error);
    }

    // ============================================================
    // READ - ObterNotasPorIdTrilha
    // ============================================================

    [Fact(DisplayName = "ObterNotasPorIdTrilhaAsync - Deve retornar notas da trilha")]
    public async Task ObterNotasPorIdTrilhaAsync_DeveRetornarComSucesso()
    {
        var notas = new List<NotaTrilhaEntity> { BuildNota() };

        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = notas,
            TotalRegistros = 1
        };

        _notaRepositoryMock
            .Setup(r => r.ObterNotasPorIdTrilhaAsync(10, 0, 10))
            .ReturnsAsync(page);

        var result = await _service.ObterNotasPorIdTrilhaAsync(10);

        Assert.True(result.IsSuccess);
    }

    [Fact(DisplayName = "ObterNotasPorIdTrilhaAsync - Deve falhar se não houver notas")]
    public async Task ObterNotasPorIdTrilhaAsync_DeveFalhar_SemNotas()
    {
        var page = new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = new List<NotaTrilhaEntity>(),
            TotalRegistros = 0
        };

        _notaRepositoryMock
            .Setup(r => r.ObterNotasPorIdTrilhaAsync(10, 0, 10))
            .ReturnsAsync(page);

        var result = await _service.ObterNotasPorIdTrilhaAsync(10);

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo de notas para essa trilha", result.Error);
    }

    // ============================================================
    // READ - ObterNotaPorId
    // ============================================================

    [Fact(DisplayName = "ObterNotaPorIdAsync - Deve retornar nota existente")]
    public async Task ObterNotaPorIdAsync_DeveRetornarComSucesso()
    {
        var nota = BuildNota();

        _notaRepositoryMock
            .Setup(r => r.ObterNotaPorIdAsync(1))
            .ReturnsAsync(nota);

        var result = await _service.ObterNotaPorIdAsync(1);

        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Value!.ValorNota);
    }

    [Fact(DisplayName = "ObterNotaPorIdAsync - Deve falhar se nota não existir")]
    public async Task ObterNotaPorIdAsync_DeveFalhar_NotFound()
    {
        _notaRepositoryMock
            .Setup(r => r.ObterNotaPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((NotaTrilhaEntity?)null);

        var result = await _service.ObterNotaPorIdAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal("Nota não encontrada", result.Error);
    }

    // ============================================================
    // CREATE
    // ============================================================

    [Fact(DisplayName = "AdicionarNotaAsync - Deve adicionar nota com sucesso")]
    public async Task AdicionarNotaAsync_DeveAdicionarComSucesso()
    {
        var dto = new NotaTrilhaDTO
        {
            IdTrilha = 10,
            IdUsuario = 20,
            ValorNota = 5,
            Observacao = "Ótima"
        };

        _trilhaRepositoryMock.Setup(r => r.ObterTrilhaPorIdAsync(dto.IdTrilha)).ReturnsAsync(BuildTrilha());
        _usuarioRepositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(dto.IdUsuario)).ReturnsAsync(BuildUsuario());
        _notaRepositoryMock.Setup(r => r.AdicionarNotaAsync(It.IsAny<NotaTrilhaEntity>()))
                           .ReturnsAsync(BuildNota(1, 5));

        var result = await _service.AdicionarNotaAsync(dto);

        Assert.True(result.IsSuccess);
        Assert.Equal(5, result.Value!.ValorNota);
    }

    [Fact(DisplayName = "AdicionarNotaAsync - Deve falhar se trilha não existe")]
    public async Task AdicionarNotaAsync_DeveFalhar_TrilhaNaoExiste()
    {
        var dto = new NotaTrilhaDTO
        {
            IdTrilha = 99,
            IdUsuario = 20,
            ValorNota = 3
        };

        _trilhaRepositoryMock.Setup(r => r.ObterTrilhaPorIdAsync(dto.IdTrilha))
                             .ReturnsAsync((TrilhaEntity?)null);

        var result = await _service.AdicionarNotaAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Trilha não encontrada", result.Error);
    }

    [Fact(DisplayName = "AdicionarNotaAsync - Deve falhar se usuário não existe")]
    public async Task AdicionarNotaAsync_DeveFalhar_UsuarioNaoExiste()
    {
        var dto = new NotaTrilhaDTO
        {
            IdTrilha = 10,
            IdUsuario = 999,
            ValorNota = 2
        };

        _trilhaRepositoryMock.Setup(r => r.ObterTrilhaPorIdAsync(dto.IdTrilha)).ReturnsAsync(BuildTrilha());
        _usuarioRepositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(dto.IdUsuario))
                              .ReturnsAsync((UsuarioEntity?)null);

        var result = await _service.AdicionarNotaAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }

    // ============================================================
    // UPDATE
    // ============================================================

    [Fact(DisplayName = "EditarNotaAsync - Deve editar nota com sucesso")]
    public async Task EditarNotaAsync_DeveEditarComSucesso()
    {
        var existente = BuildNota();
        var dto = new NotaTrilhaDTO
        {
            ValorNota = 3,
            Observacao = "Atualizada",
            IdTrilha = existente.IdTrilha,
            IdUsuario = existente.IdUsuario
        };

        var atualizado = BuildNota(1, 3);

        _notaRepositoryMock.Setup(r => r.ObterNotaPorIdAsync(1)).ReturnsAsync(existente);
        _notaRepositoryMock.Setup(r => r.EditarNotaAsync(1, It.IsAny<NotaTrilhaEntity>()))
                           .ReturnsAsync(atualizado);

        var result = await _service.EditarNotaAsync(1, dto);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value!.ValorNota);
    }

    [Fact(DisplayName = "EditarNotaAsync - Deve falhar se nota não existe")]
    public async Task EditarNotaAsync_DeveFalhar_NotFound()
    {
        _notaRepositoryMock.Setup(r => r.ObterNotaPorIdAsync(It.IsAny<long>()))
                           .ReturnsAsync((NotaTrilhaEntity?)null);

        var dto = new NotaTrilhaDTO
        {
            ValorNota = 5,
            IdTrilha = 10,
            IdUsuario = 20
        };

        var result = await _service.EditarNotaAsync(99, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Nota não encontrada", result.Error);
    }
}
