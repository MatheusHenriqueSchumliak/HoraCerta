
using HoraCerta.Application.ViewModels.Especialidade;
using HoraCerta.Application.ViewModels.Common;
using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Application.Interfaces.IServices;

public interface IEspecialidadeService
{
	Task<IEnumerable<EspecialidadeViewModel>> ObterTodos();
	Task<EspecialidadeViewModel?> ObterPorId(Guid id);
	Task<ResultadoOperacao> Criar(EspecialidadeViewModel model);
	Task Atualizar(Guid id, EspecialidadeViewModel model);
	Task Remover(Guid id);

	Task<ListaPaginada<EspecialidadeViewModel>> ObterPaginado(int pagina, int tamanhoPagina);
}
