using Moq;
using NS.Application.Dtos;
using NS.Application.Services;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.Tests.APP.Formulario;

public class FormularioServiceTests
{
    private readonly Mock<IFormularioRepository> _formularioRepositoryMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly FormularioService _formularioService;

    public FormularioServiceTests()
    {
        _formularioRepositoryMock = new Mock<IFormularioRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _formularioService = new FormularioService(_formularioRepositoryMock.Object, _usuarioRepositoryMock.Object);
    }

    private static FormularioEntity BuildFormulario(
        long id = 1,
        long idUsuario = 1,
        NivelExperiencia nivel = NivelExperiencia.INICIANTE,
        ObjetivoCarreira objetivo = ObjetivoCarreira.PRIMEIRO_EMPREGO,
        AreaTecnologia area1 = AreaTecnologia.BACKEND,
        HorasEstudo horas = HorasEstudo.DE_6_A_10H,
        string? habilidades = "C# e SQL")
    {
        return new FormularioEntity
        {
            Id = id,
            IdUsuario = idUsuario,
            NivelExperiencia = nivel,
            ObjetivoCarreira = objetivo,
            AreaTecnologia1 = area1,
            HorasEstudo = horas,
            Habilidades = habilidades
        };
    }

    private static FormularioDTO BuildFormularioDTO(long idUsuario = 1)
    {
        return new FormularioDTO
        {
            IdUsuario = idUsuario,
            NivelExperiencia = NivelExperiencia.INICIANTE,
            ObjetivoCarreira = ObjetivoCarreira.PRIMEIRO_EMPREGO,
            AreaTecnologia1 = AreaTecnologia.BACKEND,
            HorasEstudo = HorasEstudo.DE_6_A_10H,
            Habilidades = "C# e SQL"
        };
    }

    // ========================================
    // READ
    // ========================================

    [Fact(DisplayName = "ObterTodosFormulariosAsync - Deve retornar lista de formulários")]
    public async Task ObterTodosFormulariosAsync_DeveRetornarLista()
    {
        var formularios = new List<FormularioEntity> { BuildFormulario(), BuildFormulario(2) };
        var page = new PageResultModel<IEnumerable<FormularioEntity>>
        {
            Data = formularios,
            TotalRegistros = 2
        };

        _formularioRepositoryMock
            .Setup(r => r.ObterTodosFormulariosAsync(0, 10))
            .ReturnsAsync(page);

        var result = await _formularioService.ObterTodosFormulariosAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.TotalRegistros);
    }

    [Fact(DisplayName = "ObterTodosFormulariosAsync - Deve falhar se lista vazia")]
    public async Task ObterTodosFormulariosAsync_DeveFalhar_ListaVazia()
    {
        _formularioRepositoryMock
            .Setup(r => r.ObterTodosFormulariosAsync(0, 10))
            .ReturnsAsync(new PageResultModel<IEnumerable<FormularioEntity>> { Data = new List<FormularioEntity>() });

        var result = await _formularioService.ObterTodosFormulariosAsync();

        Assert.False(result.IsSuccess);
        Assert.Equal("Não existe conteudo para formularios", result.Error);
    }

    [Fact(DisplayName = "ObterFormulariosPorIdUsuarioAsync - Deve retornar formulários de usuário")]
    public async Task ObterFormulariosPorIdUsuarioAsync_DeveRetornarLista()
    {
        var formularios = new List<FormularioEntity> { BuildFormulario(idUsuario: 1), BuildFormulario(id: 2, idUsuario: 1) };
        var page = new PageResultModel<IEnumerable<FormularioEntity>> { Data = formularios, TotalRegistros = 2 };

        _formularioRepositoryMock
            .Setup(r => r.ObterFormulariosPorIdUsuarioAsync(1, 0, 10))
            .ReturnsAsync(page);

        var result = await _formularioService.ObterFormulariosPorIdUsuarioAsync(1);

        Assert.True(result.IsSuccess);
        Assert.All(result.Value!.Data, f => Assert.Equal(1, f.IdUsuario));
    }

    [Fact(DisplayName = "ObterFormularioPorIdAsync - Deve retornar formulário existente")]
    public async Task ObterFormularioPorIdAsync_DeveRetornarFormulario()
    {
        var formulario = BuildFormulario();

        _formularioRepositoryMock
            .Setup(r => r.ObterFormularioPorIdAsync(formulario.Id))
            .ReturnsAsync(formulario);

        var result = await _formularioService.ObterFormularioPorIdAsync(formulario.Id);

        Assert.True(result.IsSuccess);
        Assert.Equal(formulario.Id, result.Value!.Id);
    }

    [Fact(DisplayName = "ObterFormularioPorIdAsync - Deve falhar se formulário não existe")]
    public async Task ObterFormularioPorIdAsync_DeveFalhar_FormularioNaoExiste()
    {
        _formularioRepositoryMock
            .Setup(r => r.ObterFormularioPorIdAsync(It.IsAny<long>()))
            .ReturnsAsync((FormularioEntity?)null);

        var result = await _formularioService.ObterFormularioPorIdAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal("Formulario não encontrado", result.Error);
    }

    // ========================================
    // CREATE
    // ========================================

    [Fact(DisplayName = "AdicionarFormularioAsync - Deve adicionar formulário com sucesso")]
    public async Task AdicionarFormularioAsync_DeveAdicionarComSucesso()
    {
        var dto = BuildFormularioDTO();
        var entity = BuildFormulario();

        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(dto.IdUsuario))
            .ReturnsAsync(new UsuarioEntity { Id = dto.IdUsuario, Nome = "User", Email = "user@test.com" });

        _formularioRepositoryMock
            .Setup(r => r.AdicionarFormularioAsync(It.IsAny<FormularioEntity>()))
            .ReturnsAsync(entity);

        var result = await _formularioService.AdicionarFormularioAsync(dto);

        Assert.True(result.IsSuccess);
        Assert.Equal(dto.IdUsuario, result.Value!.IdUsuario);
    }

    [Fact(DisplayName = "AdicionarFormularioAsync - Deve falhar se usuário não existir")]
    public async Task AdicionarFormularioAsync_DeveFalhar_UsuarioNaoExiste()
    {
        var dto = BuildFormularioDTO();

        _usuarioRepositoryMock
            .Setup(r => r.ObterUsuarioPorIdAsync(dto.IdUsuario))
            .ReturnsAsync((UsuarioEntity?)null);

        var result = await _formularioService.AdicionarFormularioAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Usuário não encontrado", result.Error);
    }
}
