using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using HoraCerta.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Infrastructure.Context
{
	public class HoraCertaContext(DbContextOptions<HoraCertaContext> options) : DbContext(options)
	{
		public DbSet<Pessoa> Pessoas { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Trata datas como UTC (a partir do EF Core 6 +)
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				foreach (var property in entityType.GetProperties().Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
				{
					property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
						v => v, // Salva como está
						v => DateTime.SpecifyKind(v, DateTimeKind.Utc))); // Lê como UTC
				}
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HoraCertaContext).Assembly);

			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new PessoaMapping());
			modelBuilder.ApplyConfiguration(new EnderecoMapping());
		}
	}
}
