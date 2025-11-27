using HoraCerta.Domain.Entities.Base;
using System.Linq.Expressions;

namespace HoraCerta.Application.Interfaces.IRepository;

public interface IGenericRepository<T> where T : EntityBase
{
	Task<T?> ObterPorId(Guid id);
	Task<IEnumerable<T>> ObterTodos();
	Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicado);

	Task Adicionar(T entidade);
	Task AdicionarVarios(IEnumerable<T> entidades);

	void Remover(T entidade);
	void RemoverVarios(IEnumerable<T> entidades);

	void Atualizar(T entidade);
	Task Salvar();

	// Paginação genérica
	Task<ListaPaginada<T>> ObterPaginado(int pagina, int tamanhoPagina, Expression<Func<T, bool>>? predicado = null);
}
