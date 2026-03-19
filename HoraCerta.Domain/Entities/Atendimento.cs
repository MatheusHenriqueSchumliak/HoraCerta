using HoraCerta.Domain.Entities.Base;
using HoraCerta.Domain.Enumerables;

namespace HoraCerta.Domain.Entities
{
	public class Atendimento : EntityBase
	{
		public Guid ClienteId { get; set; }
		public Pessoa Cliente { get; set; }
		public Guid PrestadorId { get; set; }
		public Prestador Prestador { get; set; }
		public Guid ServicoId { get; set; }
		public Servico Servico { get; set; }
		public DateTime DataHoraInicio { get; set; }
		public DateTime DataHoraFim { get; set; }
		public StatusAtendimento Status { get; set; }

		public Atendimento() { }

	}
}
