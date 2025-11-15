using NS.Domain.Entities;

namespace NS.Application.Interfaces;

public interface IConteudoService
{
    Task<OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>> ObterTodosConteudosAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>> ObterConteudosPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<ConteudoEntity?>> ObterConteudoPorIdAsync(long id);
}
