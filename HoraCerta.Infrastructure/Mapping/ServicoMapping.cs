using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping
{
	public class ServicoMapping : IEntityTypeConfiguration<Servico>
	{
		public void Configure(EntityTypeBuilder<Servico> builder)
		{
			builder.ToTable("Servicos");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Nome).IsRequired().HasMaxLength(100);

			builder.Property(s => s.DuracaoMinutos).IsRequired();

			builder.Property(s => s.Preco).HasColumnType("decimal(18,2)").IsRequired();

			builder.Property(s => s.Observacao).HasMaxLength(300);

			builder.Property(s => s.DataCriacao).IsRequired();

			builder.Property(s => s.DataAtualizacao);

			// Relacionamento com Prestador (1:N)
			builder.HasOne(s => s.Prestador)
				.WithMany(p => p.Servicos)
				.HasForeignKey(s => s.PrestadorId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
