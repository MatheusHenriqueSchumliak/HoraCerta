using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Domain.Entities
{
	public class Servico : EntityBase
	{
		public Guid Id { get; set; }
		public Guid PrestadorId { get; set; }
		public Prestador Prestador { get; set; }
		public string Nome { get; set; }
		public int DuracaoMinutos { get; set; }
		public decimal Preco { get; set; }
		public string Observacao { get; set; }

		public Servico() { }

	}
}
