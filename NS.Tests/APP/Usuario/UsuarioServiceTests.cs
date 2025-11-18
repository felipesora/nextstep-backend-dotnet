using Moq;
using NS.Application.Dtos;
using NS.Application.Mappers;
using NS.Application.Services;
using NS.Domain.Entities;
using NS.Domain.Interfaces;

namespace NS.Tests.APP.Usuario;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
    }

    private static UsuarioEntity BuildUsuario(
        long id = 1,
        string nome = "Felipe",
        string email = "felipe@email.com")
    {
        return new UsuarioEntity
        {
            Id = id,
            Nome = nome,
            Email = email
        };
    }

    // ========================================
    // READ
    // ========================================

    [Fact(DisplayName = "ObterTodosUsuariosAsync - Deve retornar usuários com sucesso")]
    public async Task ObterTodosUsuariosAsync_DeveRetornarUsuarios()
    {
        var usuarios = new List<UsuarioEntity>
        {
            BuildUsuario(),
            BuildUsuario(2, "User2", "user2@email.com")
        };

        var page = new PageResultModel<IEnumerable<UsuarioEntity>>
        {
            Data = usuarios,
            TotalRegistros = 2,
            Deslocamento = 0,
            RegistrosRetornados = 2
        };

        _usuarioRepositoryMock
            .Setup(r => r.ObterTodosUsuariosAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _usuarioService.ObterTodosUsuariosAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.TotalRegistros);
    }

    [Fact(DisplayName = "ObterTodosUsuariosAsync - Deve falhar se lista estiver vazia")]
    public async Task ObterTodosUsuariosAsync_DeveFalhar_ListaVazia()
    {
        var page = new PageResultModel<IEnumerable<UsuarioEntity>>
        {
            Data = new List<UsuarioEntity>(),
            TotalRegistros = 0
        };

        _usuarioRepositoryMock
            .Setup(r => r.ObterTodosUsuariosAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _usuarioService.ObterTodosUsuariosAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para usuários", result.Error);
    }

    [Fact(DisplayName = "ObterUsuarioPorIdAsync - Deve retornar usuário existente")]
    public async Task ObterUsuarioPorIdAsync_DeveRetornarUsuario()
    {
        var usuario = BuildUsuario();

        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(usuario.Id))
            .ReturnsAsync(usuario);

        var result = await _usuarioService.ObterUsuarioPorIdAsync(usuario.Id);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(usuario.Email, result.Value!.Email);
    }

    [Fact(DisplayName = "ObterUsuarioPorIdAsync - Deve falhar se usuário não existe")]
    public async Task ObterUsuarioPorIdAsync_DeveFalhar_UsuarioNaoExiste()
    {
        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((UsuarioEntity?)null);

        var result = await _usuarioService.ObterUsuarioPorIdAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }

    // ========================================
    // CREATE
    // ========================================

    [Fact(DisplayName = "AdicionarUsuarioAsync - Deve adicionar novo usuário com sucesso")]
    public async Task AdicionarUsuarioAsync_DeveAdicionarComSucesso()
    {
        var dto = new UsuarioDTO
        {
            Nome = "Felipe",
            Email = "felipe@email.com",
            Senha = "123456"
        };
        var entity = dto.ToUsuarioEntity();

        _usuarioRepositoryMock
            .Setup(r => r.ObterTodosUsuariosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PageResultModel<IEnumerable<UsuarioEntity>>
            {
                Data = new List<UsuarioEntity>()
            });

        _usuarioRepositoryMock
            .Setup(r => r.AdicionarUsuarioAsync(It.IsAny<UsuarioEntity>()))
            .ReturnsAsync(entity);

        var result = await _usuarioService.AdicionarUsuarioAsync(dto);

        Assert.True(result.IsSuccess);
        Assert.Equal(dto.Email, result.Value!.Email);
    }

    [Fact(DisplayName = "AdicionarUsuarioAsync - Deve falhar se email já existir")]
    public async Task AdicionarUsuarioAsync_DeveFalhar_EmailDuplicado()
    {
        var dto = new UsuarioDTO
        {
            Nome = "Novo",
            Email = "email@teste.com",
            Senha = "123456"
        };

        var existente = BuildUsuario(1, "Outro", "email@teste.com");

        _usuarioRepositoryMock
            .Setup(r => r.ObterTodosUsuariosAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PageResultModel<IEnumerable<UsuarioEntity>>
            {
                Data = new List<UsuarioEntity> { existente }
            });

        var result = await _usuarioService.AdicionarUsuarioAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Já existe um usuário com este e-mail.", result.Error);
    }

    // ========================================
    // UPDATE
    // ========================================

    [Fact(DisplayName = "EditarUsuarioAsync - Deve editar usuário com sucesso")]
    public async Task EditarUsuarioAsync_DeveEditarComSucesso()
    {
        var usuario = BuildUsuario();
        var dto = new UsuarioDTO
        {
            Nome = "Atualizado",
            Email = usuario.Email,
            Senha = "123456"
        };

        var atualizado = dto.ToUsuarioEntity();
        atualizado.Id = usuario.Id;

        _usuarioRepositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
        _usuarioRepositoryMock.Setup(r => r.ExisteOutroComMesmoEmailAsync(usuario.Id, dto.Email)).ReturnsAsync(false);
        _usuarioRepositoryMock.Setup(r => r.EditarUsuarioAsync(usuario.Id, It.IsAny<UsuarioEntity>())).ReturnsAsync(atualizado);

        var result = await _usuarioService.EditarUsuarioAsync(usuario.Id, dto);

        Assert.True(result.IsSuccess);
        Assert.Equal("Atualizado", result.Value!.Nome);
    }

    [Fact(DisplayName = "EditarUsuarioAsync - Deve falhar se usuário não existe")]
    public async Task EditarUsuarioAsync_DeveFalhar_UsuarioNaoExiste()
    {
        var dto = new UsuarioDTO
        {
            Nome = "Novo",
            Email = "email@teste.com",
            Senha = "123456"
        };

        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((UsuarioEntity?)null);

        var result = await _usuarioService.EditarUsuarioAsync(99, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }

    [Fact(DisplayName = "EditarUsuarioAsync - Deve falhar email duplicado")]
    public async Task EditarUsuarioAsync_DeveFalhar_EmailDuplicado()
    {
        var usuario = BuildUsuario();
        var dto = new UsuarioDTO
        {
            Nome = "Novo",
            Email = "duplicado@teste.com",
            Senha = "123456"
        };

        _usuarioRepositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
        _usuarioRepositoryMock.Setup(r => r.ExisteOutroComMesmoEmailAsync(usuario.Id, dto.Email)).ReturnsAsync(true);

        var result = await _usuarioService.EditarUsuarioAsync(usuario.Id, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Já existe outro usuário com este e-mail", result.Error);
    }

    // ========================================
    // DELETE
    // ========================================

    [Fact(DisplayName = "DeletarUsuarioAsync - Deve deletar usuário com sucesso")]
    public async Task DeletarUsuarioAsync_DeveDeletarComSucesso()
    {
        var usuario = BuildUsuario();

        _usuarioRepositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
        _usuarioRepositoryMock
            .Setup(r => r.DeletarUsuarioAsync(usuario.Id))
            .ReturnsAsync(usuario);

        var result = await _usuarioService.DeletarUsuarioAsync(usuario.Id);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
    }

    [Fact(DisplayName = "DeletarUsuarioAsync - Deve falhar se usuário não existe")]
    public async Task DeletarUsuarioAsync_DeveFalhar_UsuarioNaoExiste()
    {
        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((UsuarioEntity?)null);

        var result = await _usuarioService.DeletarUsuarioAsync(10);

        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }
}
