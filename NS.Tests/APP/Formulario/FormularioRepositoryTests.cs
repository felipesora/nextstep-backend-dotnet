using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Infra.Data.AppData;
using NS.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.Tests.APP.Formulario;

public class FormularioRepositoryTests
{
    private static ApplicationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationContext(options);
    }

    private static FormularioEntity BuildFormulario(
        long idUsuario = 1,
        NivelExperiencia nivel = NivelExperiencia.INICIANTE,
        ObjetivoCarreira objetivo = ObjetivoCarreira.PRIMEIRO_EMPREGO,
        AreaTecnologia area1 = AreaTecnologia.BACKEND,
        AreaTecnologia? area2 = null,
        AreaTecnologia? area3 = null,
        HorasEstudo horas = HorasEstudo.DE_6_A_10H,
        string? habilidades = "C# e SQL")
    {
        return new FormularioEntity
        {
            IdUsuario = idUsuario,
            NivelExperiencia = nivel,
            ObjetivoCarreira = objetivo,
            AreaTecnologia1 = area1,
            AreaTecnologia2 = area2,
            AreaTecnologia3 = area3,
            HorasEstudo = horas,
            Habilidades = habilidades
        };
    }

    // ----------------------------------------
    // CREATE
    // ----------------------------------------
    [Fact(DisplayName = "AdicionarFormularioAsync - Deve adicionar formulário com sucesso")]
    public async Task AdicionarFormularioAsync_DeveAdicionarComSucesso()
    {
        using var context = CreateContext("AddFormDB");
        var repo = new FormularioRepository(context);

        var formulario = BuildFormulario();

        var result = await repo.AdicionarFormularioAsync(formulario);

        Assert.NotNull(result);
        Assert.Equal(HorasEstudo.DE_6_A_10H, result.HorasEstudo);
        Assert.Single(context.Formulario);
    }

    // ----------------------------------------
    // READ - GetAll
    // ----------------------------------------
    [Fact(DisplayName = "ObterTodosFormulariosAsync - Deve retornar lista paginada")]
    public async Task ObterTodosFormulariosAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetAllFormDB");
        var repo = new FormularioRepository(context);

        context.Formulario.AddRange(
            BuildFormulario(idUsuario: 1),
            BuildFormulario(idUsuario: 2)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterTodosFormulariosAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.Equal(2, result.Data.Count());
    }

    // ----------------------------------------
    // READ - GetById
    // ----------------------------------------
    [Fact(DisplayName = "ObterFormularioPorIdAsync - Deve retornar formulário por ID")]
    public async Task ObterFormularioPorIdAsync_DeveRetornarFormulario()
    {
        using var context = CreateContext("GetFormByIdDB");
        var repo = new FormularioRepository(context);

        var formulario = BuildFormulario(idUsuario: 1);
        context.Formulario.Add(formulario);
        await context.SaveChangesAsync();

        var result = await repo.ObterFormularioPorIdAsync(formulario.Id);

        Assert.NotNull(result);
        Assert.Equal(HorasEstudo.DE_6_A_10H, result.HorasEstudo);
    }

    [Fact(DisplayName = "ObterFormularioPorIdAsync - Deve retornar null se não existir")]
    public async Task ObterFormularioPorIdAsync_DeveRetornarNullSeNaoExistir()
    {
        using var context = CreateContext("GetFormNullDB");
        var repo = new FormularioRepository(context);

        var result = await repo.ObterFormularioPorIdAsync(999);

        Assert.Null(result);
    }

    // ----------------------------------------
    // READ - GetByUsuario
    // ----------------------------------------
    [Fact(DisplayName = "ObterFormulariosPorIdUsuarioAsync - Deve retornar formulários de um usuário")]
    public async Task ObterFormulariosPorIdUsuarioAsync_DeveRetornarLista()
    {
        using var context = CreateContext("GetFormByUserDB");
        var repo = new FormularioRepository(context);

        context.Formulario.AddRange(
            BuildFormulario(idUsuario: 1),
            BuildFormulario(idUsuario: 2),
            BuildFormulario(idUsuario: 1)
        );
        await context.SaveChangesAsync();

        var result = await repo.ObterFormulariosPorIdUsuarioAsync(1);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalRegistros);
        Assert.All(result.Data, f => Assert.Equal(1, f.IdUsuario));
    }
}
