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
		public DbSet<Prestador> Prestadores { get; set; }
		public DbSet<Servico> Servicos { get; set; }
		public DbSet<Atendimento> Atendimentos { get; set; }
		public DbSet<HorarioAtendimento> HorariosAtendimentos { get; set; }
		public DbSet<Especialidade> Especialidades { get; set; }

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
			modelBuilder.ApplyConfiguration(new PrestadorMapping());
			modelBuilder.ApplyConfiguration(new ServicoMapping());
			modelBuilder.ApplyConfiguration(new AtendimentoMapping());
			modelBuilder.ApplyConfiguration(new HorarioAtendimentoMapping());
			modelBuilder.ApplyConfiguration(new EspecialidadeMapping());
		}
	}
}
