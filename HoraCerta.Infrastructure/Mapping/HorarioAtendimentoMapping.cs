using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping
{
	public class HorarioAtendimentoMapping : IEntityTypeConfiguration<HorarioAtendimento>
	{
		public void Configure(EntityTypeBuilder<HorarioAtendimento> builder)
		{
			builder.ToTable("HorariosAtendimentos");

			builder.HasKey(h => h.Id);

			builder.Property(h => h.DiaSemana).IsRequired();

			builder.Property(h => h.HoraInicio).IsRequired();

			builder.Property(h => h.HoraFim).IsRequired();

			builder.Property(h => h.DataCriacao).IsRequired();

			builder.Property(h => h.DataAtualizacao);

			// Relacionamento com Prestador (1:N)
			builder.HasOne(h => h.Prestador)
				.WithMany(p => p.Horarios)
				.HasForeignKey(h => h.PrestadorId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
