using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
	public void Configure(EntityTypeBuilder<Pessoa> builder)
	{
		builder.ToTable("Pessoas");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Nome).IsRequired().HasMaxLength(200);

		builder.Property(p => p.SobreNome).HasMaxLength(200);

		builder.Property(p => p.Telefone).HasMaxLength(15);

		builder.Property(p => p.Celular).IsRequired().HasMaxLength(15);

		builder.Property(p => p.Cpf).IsRequired().HasMaxLength(11);

		builder.Property(p => p.DataNascimento).IsRequired();

		builder.Property(p => p.Observacao).HasMaxLength(500);

		builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

		//relação 1:1 — Pessoa é o principal, Endereco é dependente
		builder.HasOne(p => p.Endereco)
			   .WithOne(e => e.Pessoa)
			   .HasForeignKey<Endereco>(e => e.PessoaId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
