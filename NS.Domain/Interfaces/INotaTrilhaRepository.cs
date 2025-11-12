using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface INotaTrilhaRepository
{
    Task<PageResultModel<IEnumerable<NotaTrilhaEntity>>> ObterTodasNotasAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<PageResultModel<IEnumerable<NotaTrilhaEntity>>> ObterNotasPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10);
    Task<NotaTrilhaEntity?> ObterTrilhaPorIdAsync(long id);
    Task<NotaTrilhaEntity?> AdicionarNotaAsync(NotaTrilhaEntity nota);
    Task<NotaTrilhaEntity?> EditarNotaAsync(long id, NotaTrilhaEntity novaNota);
}
