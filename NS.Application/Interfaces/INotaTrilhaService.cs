using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Interfaces;

public interface INotaTrilhaService
{
    Task<OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>> ObterTodasNotasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>> ObterNotasPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<NotaTrilhaEntity?>> ObterNotaPorIdAsync(long id);
    Task<OperationResult<NotaTrilhaEntity?>> AdicionarNotaAsync(NotaTrilhaDTO notaDTO);
    Task<OperationResult<NotaTrilhaEntity?>> EditarNotaAsync(long id, NotaTrilhaDTO novaNotaDTO);
}
