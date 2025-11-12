using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Domain.Interfaces;
using System.Net;

namespace NS.Application.Services;

public class TrilhaService : ITrilhaService
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly ITrilhaRepository _trilhaRepository;

    public TrilhaService(ITrilhaRepository trilhaRepository)
    {
        _trilhaRepository = trilhaRepository;
    }

    #endregion

    #region :: READ

    public async Task<OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>> ObterTodasTrilhasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _trilhaRepository.ObterTodasTrilhasAsync(deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Failure("Não existe conteudo para trilhas", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Failure("Ocorreu um erro ao buscar as trilhas");
        }
    }

    public async Task<OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>> ObterTrilhasAtivasAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _trilhaRepository.ObterTrilhasAtivasAsync(deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Failure("Não existe conteudo para trilhas ativas", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<TrilhaEntity>>>.Failure("Ocorreu um erro ao buscar as trilhas ativas");
        }
    }

    public async Task<OperationResult<TrilhaEntity?>> ObterTrilhaPorIdAsync(long id)
    {
        try
        {
            var result = await _trilhaRepository.ObterTrilhaPorIdAsync(id);

            if (result is null)
                return OperationResult<TrilhaEntity?>.Failure("Trilha não encontrada", (int)HttpStatusCode.NotFound);

            return OperationResult<TrilhaEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<TrilhaEntity?>.Failure("Ocorreu um erro ao buscar a trilha");
        }
    }

    #endregion
}
