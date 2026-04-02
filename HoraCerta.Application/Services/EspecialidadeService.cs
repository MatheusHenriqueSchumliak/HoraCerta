using HoraCerta.Application.ViewModels.Especialidade;
using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Application.Interfaces.IServices;
using HoraCerta.Application.Factories;
using HoraCerta.Domain.Entities.Base;
using HoraCerta.Application.ViewModels.Common;

namespace HoraCerta.Application.Services;

public class EspecialidadeService(IEspecialidadeRepository especialidadeRepository) : IEspecialidadeService
{
	#region Construtor
	private readonly IEspecialidadeRepository _especialidadeRepository = especialidadeRepository;
	#endregion

	#region C.R.U.D.
	public Task<IEnumerable<EspecialidadeViewModel>> ObterTodos()
	{
		throw new NotImplementedException();
	}

	public Task<EspecialidadeViewModel?> ObterPorId(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<ResultadoOperacao> Criar(EspecialidadeViewModel model)
	{
		throw new NotImplementedException();
	}

	public Task Atualizar(Guid id, EspecialidadeViewModel model)
	{
		throw new NotImplementedException();
	}

	public Task Remover(Guid id)
	{
		throw new NotImplementedException();
	}

	#endregion C.R.U.D.

	#region Paginação
	public async Task<ListaPaginada<EspecialidadeViewModel>> ObterPaginado(int pagina, int tamanhoPagina)
	{
		var paginado = await _especialidadeRepository.ObterPaginado(pagina, tamanhoPagina).ConfigureAwait(false);
		var itensViewModel = paginado.Items.Select(EspecialidadeFactory.ParaViewModel).ToList();
		return new ListaPaginada<EspecialidadeViewModel>(itensViewModel, paginado.TotalItens, paginado.NumeroPagina, paginado.TamanhoPagina);
	}
	#endregion
}
