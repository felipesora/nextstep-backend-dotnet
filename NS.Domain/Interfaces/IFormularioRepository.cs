using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface IFormularioRepository
{
    Task<PageResultModel<IEnumerable<FormularioEntity>>> ObterTodosFormulariosAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<PageResultModel<IEnumerable<FormularioEntity>>> ObterFormulariosPorIdUsuarioAsync(long idUsuario, int deslocamento = 0, int registrosRetornados = 10);
    Task<FormularioEntity?> ObterFormularioPorIdAsync(long id);
    Task<FormularioEntity?> AdicionarFormularioAsync(FormularioEntity formulario);
}
