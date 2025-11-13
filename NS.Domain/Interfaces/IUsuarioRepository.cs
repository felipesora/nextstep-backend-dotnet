using NS.Domain.Entities;

namespace NS.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodosUsuariosAsync(int deslocamento = 0, int registrosRetornados = 10);
    Task<UsuarioEntity?> ObterUsuarioPorIdAsync(long id);
    Task<UsuarioEntity?> AdicionarUsuarioAsync(UsuarioEntity usuario);
    Task<UsuarioEntity?> EditarUsuarioAsync(long id, UsuarioEntity novoUsuario);
    Task<UsuarioEntity?> DeletarUsuarioAsync(long id);
    Task<bool> ExisteOutroComMesmoEmailAsync(long id, string email);
}
