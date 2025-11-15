using NS.Application.Interfaces;
using NS.Domain.Entities;
using NS.Domain.Interfaces;
using System.Net;

namespace NS.Application.Services;

public class ConteudoService : IConteudoService
{
    #region :: INJEÇÃO DE DEPENDÊNCIA

    private readonly IConteudoRepository _conteudoRepository;

    public ConteudoService(IConteudoRepository conteudoRepository)
    {
        _conteudoRepository = conteudoRepository;
    }

    #endregion

    #region :: READ

    public async Task<OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>> ObterTodosConteudosAsync(int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _conteudoRepository.ObterTodosConteudosAsync(deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Failure("Não existe conteudo para conteudos", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Failure("Ocorreu um erro ao buscar os conteudos");
        }
    }

    public async Task<OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>> ObterConteudosPorIdTrilhaAsync(long idTrilha, int deslocamento = 0, int registrosRetornados = 10)
    {
        try
        {
            var result = await _conteudoRepository.ObterConteudosPorIdTrilhaAsync(idTrilha, deslocamento, registrosRetornados);

            if (!result.Data.Any())
                return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Failure("Não existe conteudo para conteudos dessa trilha", (int)HttpStatusCode.NotFound);

            return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<PageResultModel<IEnumerable<ConteudoEntity>>>.Failure("Ocorreu um erro ao buscar os conteudos dessa trilha");
        }
    }

    public async Task<OperationResult<ConteudoEntity?>> ObterConteudoPorIdAsync(long id)
    {
        try
        {
            var result = await _conteudoRepository.ObterConteudoPorIdAsync(id);

            if (result is null)
                return OperationResult<ConteudoEntity?>.Failure("Conteudo não encontrado", (int)HttpStatusCode.NotFound);

            return OperationResult<ConteudoEntity?>.Success(result);
        }
        catch (Exception)
        {
            return OperationResult<ConteudoEntity?>.Failure("Ocorreu um erro ao buscar o conteudo");
        }
    }

    #endregion
}
