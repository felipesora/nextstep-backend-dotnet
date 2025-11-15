using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;
using NS.Domain.Enums;
using NS.Domain.Interfaces;
using NS.Infra.Data.AppData;

namespace NS.Infra.Data.Repositories;

public class ConteudoRepository : IConteudoRepository
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ApplicationContext _context;

    public ConteudoRepository(ApplicationContext context)
    {
        _context = context;
    }

    #endregion

    #region :: READ

    public async Task<PageResultModel<IEnumerable<ConteudoEntity>>> ObterTodosConteudosAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Conteudo.CountAsync();

        var result = await _context
            .Conteudo
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };

    }

    public async Task<PageResultModel<IEnumerable<ConteudoEntity>>> ObterConteudosPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        var totalRegistros = await _context.Conteudo.Where(c => c.IdTrilha == idTrilha).CountAsync();

        var result = await _context
            .Conteudo
            .Where(c => c.IdTrilha == idTrilha)
            .OrderBy(m => m.Id)
            .Skip(deslocamento)
            .Take(registrosRetornados)
            .ToListAsync();

        return new PageResultModel<IEnumerable<ConteudoEntity>>
        {
            Data = result,
            Deslocamento = deslocamento,
            RegistrosRetornados = registrosRetornados,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<ConteudoEntity?> ObterConteudoPorIdAsync(long id)
    {
        var result = await _context
            .Conteudo
            .FirstOrDefaultAsync(m => m.Id == id);

        return result;
    }

    #endregion
}
