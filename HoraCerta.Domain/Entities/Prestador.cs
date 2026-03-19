using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Domain.Entities
{
	public class Prestador : EntityBase
	{
		public Guid PessoaId { get; set; }
		public Pessoa Pessoa { get; set; }
		public Guid EspecialidadeId { get; set; }
		public Especialidade Especialidade { get; set; }
		public string Descricao { get; set; }
		public string Observacao { get; set; }
		public ICollection<Servico> Servicos { get; set; }
		public ICollection<HorarioAtendimento> Horarios { get; set; }
		public Prestador() { }

	}
}
