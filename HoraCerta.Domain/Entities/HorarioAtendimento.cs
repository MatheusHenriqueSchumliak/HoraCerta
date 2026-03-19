using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Domain.Entities
{
	public class HorarioAtendimento : EntityBase
	{
		public Guid PrestadorId { get; set; }
		public Prestador Prestador { get; set; }
		public DayOfWeek DiaSemana { get; set; }
		public TimeSpan HoraInicio { get; set; }
		public TimeSpan HoraFim { get; set; }

		public HorarioAtendimento() { }
	}
}
