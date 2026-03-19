using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Domain.Entities
{
	public class Especialidade : EntityBase
	{
		public string Nome { get; set; }
		public string? Descricao { get; set; }
		public ICollection<Prestador> Prestadores { get; set; }

		public Especialidade() { }
	}
}
