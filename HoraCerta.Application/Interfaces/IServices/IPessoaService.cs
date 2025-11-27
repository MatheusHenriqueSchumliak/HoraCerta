using HoraCerta.Application.ViewModels.Pessoa;
using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Application.Interfaces.IServices;

public interface IPessoaService
{
	Task<IEnumerable<PessoaViewModel>> ObterTodos();
	Task<PessoaViewModel?> ObterPorId(Guid id);
	Task<Guid> Criar(PessoaViewModel model);
	Task Atualizar(Guid id, PessoaViewModel model);
	Task Remover(Guid id);

	Task<ListaPaginada<PessoaViewModel>> ObterPaginado(int pagina, int tamanhoPagina);
}

