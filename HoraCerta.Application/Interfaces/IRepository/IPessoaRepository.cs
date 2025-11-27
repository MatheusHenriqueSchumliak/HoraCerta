using HoraCerta.Domain.Entities;

namespace HoraCerta.Application.Interfaces.IRepository;

public interface IPessoaRepository : IGenericRepository<Pessoa>
{
	Task<Pessoa?> ObterPorCpf(string cpf);
}
