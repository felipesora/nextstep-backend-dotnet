using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface IConteudoRepository
{
    Task<PageResultModel<IEnumerable<ConteudoEntity>>> ObterTodosConteudosAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<PageResultModel<IEnumerable<ConteudoEntity>>> ObterConteudosPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10);
    Task<ConteudoEntity?> ObterConteudoPorIdAsync(long id);
}
