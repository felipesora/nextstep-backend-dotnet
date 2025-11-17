using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Domain.Interfaces;
using NS.Infra.Data.AppData;

namespace NS.Infra.Data.Repositories;

public class TrilhaRepository : ITrilhaRepository
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ApplicationContext _context;

    public TrilhaRepository(ApplicationContext context)
    {
        _context = context;
    }

    #endregion

    #region :: READ

    public async Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterTodasTrilhasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Trilha.CountAsync();

        var result = await _context
            .Trilha
            .Include(t => t.Conteudos)
            .Include(t => t.Notas)
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<TrilhaEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterTrilhasAtivasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Trilha.Where(t => t.Status == StatusTrilha.ATIVA).CountAsync();

        var result = await _context
            .Trilha
            .Include(t => t.Conteudos)
            .Include(t => t.Notas)
            .Where(t => t.Status == StatusTrilha.ATIVA)
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<TrilhaEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<TrilhaEntity?> ObterTrilhaPorIdAsync(long id)
    {
        var result = await _context
            .Trilha
            .FirstOrDefaultAsync(m => m.Id == id);

        return result;
    }

    #endregion
}
