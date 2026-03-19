using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping
{
	public class PrestadorMapping : IEntityTypeConfiguration<Prestador>
	{
		public void Configure(EntityTypeBuilder<Prestador> builder)
		{
			builder.ToTable("Prestadores");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Descricao).HasMaxLength(300);

			builder.Property(p => p.Observacao).HasMaxLength(300);

			builder.Property(p => p.DataCriacao).IsRequired();

			builder.Property(p => p.DataAtualizacao);

			// Relacionamento com Pessoa (1:1)
			builder.HasOne(p => p.Pessoa)
				.WithMany()
				.HasForeignKey(p => p.PessoaId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relacionamento com Especialidade (1:N)
			builder.HasOne(p => p.Especialidade)
				.WithMany(e => e.Prestadores)
				.HasForeignKey(p => p.EspecialidadeId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relacionamento com Servicos (1:N)
			builder.HasMany(p => p.Servicos)
				.WithOne(s => s.Prestador)
				.HasForeignKey(s => s.PrestadorId);

			// Relacionamento com Horarios (1:N)
			builder.HasMany(p => p.Horarios)
				.WithOne(h => h.Prestador)
				.HasForeignKey(h => h.PrestadorId);

		}
	}
}
