using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping
{
	public class EspecialidadeMapping : IEntityTypeConfiguration<Especialidade>
	{
		public void Configure(EntityTypeBuilder<Especialidade> builder)
		{
			builder.ToTable("Especialidades");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);

			builder.Property(e => e.Descricao).HasMaxLength(300);

			builder.Property(e => e.DataCriacao).IsRequired();

			builder.Property(e => e.DataAtualizacao);

			// Relacionamento com Prestadores (1:N)
			builder.HasMany(e => e.Prestadores)
				.WithOne(p => p.Especialidade)
				.HasForeignKey(p => p.EspecialidadeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
