using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Repository;

public class PessoaRepository : GenericRepository<Pessoa>, IPessoaRepository
{
	public PessoaRepository(HoraCertaContext context) : base(context) { }

	public async Task<Pessoa?> ObterPorCpf(string cpf)
	{
		return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Cpf == cpf).ConfigureAwait(false);
	}

}
