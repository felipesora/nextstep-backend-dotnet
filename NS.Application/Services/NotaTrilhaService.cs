using NS.Application.Dtos;
using NS.Application.Interfaces;
using NS.Application.Mappers;
using NS.Domain.Entities;
using NS.Domain.Interfaces;
using System.Net;

namespace NS.Application.Services;

public class NotaTrilhaService : INotaTrilhaService
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly INotaTrilhaRepository _notaTrilhaRepository;

    public NotaTrilhaService(INotaTrilhaRepository notaTrilhaRepository)
    {
        _notaTrilhaRepository = notaTrilhaRepository;
    }

    #endregion

    #region :: READ

    public async Task<OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>> ObterTodasNotasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _notaTrilhaRepository.ObterTodasNotasAsync(deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Failure("Não existe conteudo para notas", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Failure("Ocorreu um erro ao buscar as notas");
        }
    }

    public async Task<OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>> ObterNotasPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _notaTrilhaRepository.ObterNotasPorIdTrilhaAsync(idTrilha, deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Failure("Não existe conteudo de notas para essa trilha", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<NotaTrilhaEntity>>>.Failure("Ocorreu um erro ao buscar as notas para essa trilha");
        }
    }

    public async Task<OperationResult<NotaTrilhaEntity?>> ObterNotaPorIdAsync(long id)
    {
        try
        {
            var result = await _notaTrilhaRepository.ObterNotaPorIdAsync(id);

            if (result is null)
                return OperationResult<NotaTrilhaEntity?>.Failure("Nota não encontrada", (int)HttpStatusCode.NotFound);

            return OperationResult<NotaTrilhaEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<NotaTrilhaEntity?>.Failure("Ocorreu um erro ao buscar a nota");
        }
    }

    #endregion

    #region :: CREATE

    public async Task<OperationResult<NotaTrilhaEntity?>> AdicionarNotaAsync(NotaTrilhaDTO notaDTO)
    {
        try
        {
            var result = await _notaTrilhaRepository.AdicionarNotaAsync(notaDTO.ToNotaEntity());

            return OperationResult<NotaTrilhaEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<NotaTrilhaEntity?>.Failure("Ocorreu um erro ao salvar a nota");
        }
    }

    #endregion

    #region :: UPDATE

    public async Task<OperationResult<NotaTrilhaEntity?>> EditarNotaAsync(long id, NotaTrilhaDTO novaNotaDTO)
    {
        try
        {
            var existente = await _notaTrilhaRepository.ObterNotaPorIdAsync(id);

            if (existente is null) return OperationResult<NotaTrilhaEntity?>.Failure("Nota não encontrada", (int)HttpStatusCode.NotFound);

            var entidade = novaNotaDTO.ToNotaEntity();
            entidade.Id = id;
            var atualizado = await _notaTrilhaRepository.EditarNotaAsync(id, entidade);

            return OperationResult<NotaTrilhaEntity?>.Success(atualizado);
        }
        catch (Exception ex)
        {
            return OperationResult<NotaTrilhaEntity?>.Failure("Ocorreu um erro ao editar a nota");
        }
    }

    #endregion
}
