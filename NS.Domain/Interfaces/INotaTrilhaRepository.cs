using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface INotaTrilhaRepository
{
    Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterTodasNotasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterNotasPorIdTrilhaAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<TrilhaEntity?> ObterTrilhaPorIdAsync(long id);
    Task<TrilhaEntity?> AdicionarNotaAsync(TrilhaEntity nota);
    Task<TrilhaEntity?> EditarNotaAsync(long id, TrilhaEntity novaNota);
}
