using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Mapping;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
	public void Configure(EntityTypeBuilder<Endereco> builder)
	{
		builder.ToTable("Enderecos");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.PessoaId).IsRequired();

		builder.Property(e => e.CEP).IsRequired().HasMaxLength(9);

		builder.Property(e => e.Rua).IsRequired().HasMaxLength(256);

		builder.Property(e => e.Numero).IsRequired().HasMaxLength(8);

		builder.Property(e => e.Bairro).IsRequired().HasMaxLength(256);

		builder.Property(e => e.Cidade).IsRequired().HasMaxLength(256);

		builder.Property(e => e.Estado).IsRequired().HasMaxLength(256);

		builder.Property(e => e.Complemento).HasMaxLength(256);

	}
}
