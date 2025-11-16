using NS.Application.Dtos;
using NS.Domain.Entities;

namespace NS.Application.Interfaces;

public interface IFormularioService
{
    Task<OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>> ObterTodosFormulariosAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>> ObterFormulariosPorIdUsuarioAsync(long idUsuario, int deslocamento = 0, int registrosRetornados = 10);
    Task<OperationResult<FormularioEntity?>> ObterFormularioPorIdAsync(long id);
    Task<OperationResult<FormularioEntity?>> AdicionarFormularioAsync(FormularioDTO formularioDTO);
}
