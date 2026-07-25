using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Domain.Entities.Base;
using HoraCerta.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HoraCerta.Infrastructure.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
{
	#region Construtor
	protected readonly HoraCertaContext _context;
	protected readonly DbSet<T> _dbSet;

	public GenericRepository(HoraCertaContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}
	#endregion Construtor

	public virtual async Task<T?> ObterPorId(Guid id)
	{
		try
		{
			return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao obter entidade {typeof(T).Name} por ID {id}", ex);
		}
	}

	public virtual async Task<IEnumerable<T>> ObterTodos()
	{
		try
		{
			return await _dbSet.ToListAsync();
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	public virtual async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicado)
	{
		try
		{
			return await _dbSet.Where(predicado).ToListAsync();
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	public virtual async Task Adicionar(T entidade)
	{
		try
		{
			await _dbSet.AddAsync(entidade);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao adicionar entidade {typeof(T).Name}", ex);
		}
	}

	public virtual async Task AdicionarVarios(IEnumerable<T> entidades)
	{
		try
		{
			await _dbSet.AddRangeAsync(entidades);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao adicionar múltiplas entidades {typeof(T).Name}", ex);
		}
	}

	public virtual void Remover(T entidade)
	{
		try
		{
			_dbSet.Remove(entidade);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao remover entidade {typeof(T).Name}", ex);
		}
	}

	public virtual void RemoverVarios(IEnumerable<T> entidades)
	{
		try
		{
			_dbSet.RemoveRange(entidades);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao remover múltiplas entidades {typeof(T).Name}", ex);
		}
	}

	public virtual void Atualizar(T entidade)
	{
		try
		{
			_dbSet.Update(entidade);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Erro ao atualizar entidade {typeof(T).Name}", ex);
		}
	}

	public virtual async Task Salvar()
	{
		try
		{
			Console.WriteLine($"🔍 Tentando salvar no banco...");
			Console.WriteLine($"📊 Connection String: {_context.Database.GetConnectionString()}");

			// Testa a conexão antes de salvar
			var canConnect = await _context.Database.CanConnectAsync();
			Console.WriteLine($"✅ Conexão disponível: {canConnect}");

			await _context.SaveChangesAsync();
			Console.WriteLine($"✅ Salvo com sucesso!");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"❌ ERRO ao salvar: {ex.Message}");
			if (ex.InnerException != null)
				Console.WriteLine($"❌ Inner Exception: {ex.InnerException.Message}");
			throw;
		}
	}


	// Implementação de paginação genérica
	public async Task<ListaPaginada<T>> ObterPaginado(int pagina, int tamanhoPagina, Expression<Func<T, bool>>? predicado = null)
	{
		// normaliza parâmetros
		if (pagina < 1) pagina = 1;
		if (tamanhoPagina < 1) tamanhoPagina = 10;

		try
		{
			// monta query base (sem tracking para leitura)
			IQueryable<T> query = _dbSet.AsNoTracking();
			if (predicado is not null) query = query.Where(predicado);

			// conta total e obtém página
			var totalItens = await query.CountAsync().ConfigureAwait(false);
			var items = await query
				.Skip((pagina - 1) * tamanhoPagina)
				.Take(tamanhoPagina)
				.ToListAsync()
				.ConfigureAwait(false);

			return new ListaPaginada<T>(items, totalItens, pagina, tamanhoPagina);
		}
		catch (Microsoft.Data.SqlClient.SqlException ex)
		{
			// Se for erro de tabela inexistente ou banco inacessível, retorna lista vazia
			return new ListaPaginada<T>(new List<T>(), 0, pagina, tamanhoPagina);
		}
	}
}
