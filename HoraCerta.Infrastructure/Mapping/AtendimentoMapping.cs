using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping
{
	public class AtendimentoMapping : IEntityTypeConfiguration<Atendimento>
	{
		public void Configure(EntityTypeBuilder<Atendimento> builder)
		{
			builder.ToTable("Atendimentos");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.DataHoraInicio).IsRequired();

			builder.Property(a => a.DataHoraFim).IsRequired();

			builder.Property(a => a.Status).IsRequired();

			builder.Property(a => a.DataCriacao).IsRequired();

			builder.Property(a => a.DataAtualizacao);

			// Relacionamento com Cliente (Pessoa) muitos-para-um (N:1)
			builder.HasOne(a => a.Cliente)
				.WithMany()
				.HasForeignKey(a => a.ClienteId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relacionamento com Prestador muitos-para-um (N:1)
			builder.HasOne(a => a.Prestador)
				.WithMany()
				.HasForeignKey(a => a.PrestadorId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relacionamento com Servico muitos-para-um (N:1)
			builder.HasOne(a => a.Servico)
				.WithMany()
				.HasForeignKey(a => a.ServicoId)
				.OnDelete(DeleteBehavior.Restrict);

			
		}
	}
}
