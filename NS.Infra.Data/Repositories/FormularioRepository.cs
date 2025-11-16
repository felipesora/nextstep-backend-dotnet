using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Interfaces;
using NS.Infra.Data.AppData;

namespace NS.Infra.Data.Repositories;

public class FormularioRepository : IFormularioRepository
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ApplicationContext _context;

    public FormularioRepository(ApplicationContext context)
    {
        _context = context;
    }

    #endregion

    #region :: READ

    public async Task<PageResultModel<IEnumerable<FormularioEntity>>> ObterTodosFormulariosAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Formulario.CountAsync();

        var result = await _context
            .Formulario
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<FormularioEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };

    }

    public async Task<PageResultModel<IEnumerable<FormularioEntity>>> ObterFormulariosPorIdUsuarioAsync(long idUsuario, int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Formulario.Where(f => f.IdUsuario == idUsuario).CountAsync();

        var result = await _context
            .Formulario
            .Where(f => f.IdUsuario == idUsuario)
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<FormularioEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<FormularioEntity?> ObterFormularioPorIdAsync(long id)
    {
        var result = await _context
            .Formulario
            .FirstOrDefaultAsync(m => m.Id == id);

        return result;
    }

    #endregion

    #region :: CREATE
    public async Task<FormularioEntity?> AdicionarFormularioAsync(FormularioEntity formulario)
    {
        _context.Formulario.Add(formulario);
        await _context.SaveChangesAsync();

        return formulario;
    }

    #endregion
}
