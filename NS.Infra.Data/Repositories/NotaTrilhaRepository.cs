using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Domain.Interfaces;
using NS.Infra.Data.AppData;

namespace NS.Infra.Data.Repositories;

public class NotaTrilhaRepository : INotaTrilhaRepository
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ApplicationContext _context;

    public INotaTrilhaRepository(ApplicationContext context)
    {
        _context = context;
    }

    #endregion

    #region :: READ

    public async Task<PageResultModel<IEnumerable<NotaTrilhaEntity>>> ObterTodasNotasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Nota.CountAsync();

        var result = await _context
            .Nota
            .OrderBy(n => n.ValorNota)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<PageResultModel<IEnumerable<NotaTrilhaEntity>>> ObterNotasPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Nota.Where(n => n.IdTrilha == idTrilha).CountAsync();

        var result = await _context
            .Nota
            .Where(n => n.IdTrilha == idTrilha)
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<NotaTrilhaEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<NotaTrilhaEntity?> ObterNotaPorIdAsync(long id)
    {
        var result = await _context
            .Nota
            .FirstOrDefaultAsync(m => m.Id == id);

        return result;
    }

    #endregion

    #region :: CREATE
    public async Task<NotaTrilhaEntity?> AdicionarNotaAsync(NotaTrilhaEntity nota)
    {
        _context.Nota.Add(nota);
        await _context.SaveChangesAsync();

        return nota;
    }

    #endregion

    #region :: UPDATE

    public async Task<NotaTrilhaEntity?> EditarNotaAsync(long id, NotaTrilhaEntity novaNota)
    {
        var notaExistente = await _context.Nota.FirstOrDefaultAsync(c => c.Id == id);

        if (notaExistente is null)
            return null;

        notaExistente.ValorNota = novaNota.ValorNota;
        notaExistente.Observacao = novaNota.Observacao;
        notaExistente.IdTrilha = novaNota.IdTrilha;

        await _context.SaveChangesAsync();
        return notaExistente;
    }

    #endregion
}
