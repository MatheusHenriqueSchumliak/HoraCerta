using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Infrastructure.Context;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Repository;

public class EspecialidadeRepository : GenericRepository<Especialidade>, IEspecialidadeRepository
{
	public EspecialidadeRepository(HoraCertaContext context) : base(context) { }

}
