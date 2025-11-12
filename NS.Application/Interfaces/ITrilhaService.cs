using NS.Domain.Entities;

namespace NS.Application.Interfaces;

public interface ITrilhaService
{
    Task<OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>> ObterTodasTrilhasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>> ObterTrilhasAtivasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<TrilhaEntity?>> ObterTrilhaPorIdAsync(long id);
}
