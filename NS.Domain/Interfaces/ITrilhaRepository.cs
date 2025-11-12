using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface ITrilhaRepository
{
    Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterTodasTrilhasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<PageResultModel<IEnumerable<TrilhaEntity>>> ObterTrilhasAtivasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<TrilhaEntity?> ObterTrilhaPorIdAsync(long id);
}
