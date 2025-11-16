using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Application.Mappers;
using NS.Domain.Entities;
using NS.Domain.Interfaces;
using System.Net;

namespace NS.Application.Services;

public class FormularioService : IFormularioService
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly IFormularioRepository _formularioRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public FormularioService(IFormularioRepository formularioRepository, IUsuarioRepository usuarioRepository)
    {
        _formularioRepository = formularioRepository;
        _usuarioRepository = usuarioRepository;
    }

    #endregion

    #region :: READ

    public async Task<OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>> ObterTodosFormulariosAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _formularioRepository.ObterTodosFormulariosAsync(deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Failure("Não existe conteudo para formularios", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Failure("Ocorreu um erro ao buscar os formularios");
        }
    }

    public async Task<OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>> ObterFormulariosPorIdUsuarioAsync(long idUsuario, int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _formularioRepository.ObterFormulariosPorIdUsuarioAsync(idUsuario, deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Failure("Não existe conteudo de formularios para este usuário", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<FormularioEntity>>>.Failure("Ocorreu um erro ao buscar formularios para este usuário");
        }
    }

    public async Task<OperationResult<FormularioEntity?>> ObterFormularioPorIdAsync(long id)
    {
        try
        {
            var result = await _formularioRepository.ObterFormularioPorIdAsync(id);

            if (result is null)
                return OperationResult<FormularioEntity?>.Failure("Formulario não encontrado", (int)HttpStatusCode.NotFound);

            return OperationResult<FormularioEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<FormularioEntity?>.Failure("Ocorreu um erro ao buscar o formulario");
        }
    }

    #endregion

    #region :: CREATE

    public async Task<OperationResult<FormularioEntity?>> AdicionarFormularioAsync(FormularioDTO formularioDTO)
    {
        try
        {

            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(formularioDTO.IdUsuario);

            if (usuario is null)
                return OperationResult<FormularioEntity?>.Failure("Usuário não encontrado", (int)HttpStatusCode.NotFound);

            var result = await _formularioRepository.AdicionarFormularioAsync(formularioDTO.ToFormularioEntity());

            return OperationResult<FormularioEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<FormularioEntity?>.Failure("Ocorreu um erro ao salvar o formulario");
        }
    }

    #endregion
}
