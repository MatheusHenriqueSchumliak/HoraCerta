using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Application.Interfaces.IServices;
using HoraCerta.Application.Services;
using HoraCerta.Infrastructure.Context;
using HoraCerta.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HoraCerta.Infrastructure.DependencyInjection
{
	public static class InfrastructureServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("HoraCertaConnection");

			// 🔍 DIAGNÓSTICO - Adicione estas linhas
			Console.WriteLine("=================================");
			Console.WriteLine($"Connection String: {connectionString}");
			Console.WriteLine("=================================");

			services.AddDbContextPool<HoraCertaContext>(options =>
				options.UseSqlServer(connectionString, sql =>
				{
					// Mantenha comentado por enquanto
				})
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
			);

			services.AddMemoryCache();
			services.AddHealthChecks().AddDbContextCheck<HoraCertaContext>("HoraCerta DB");

			#region Data Repositories
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
			#endregion);

			#region Application Services		
			services.AddScoped<IPessoaService, PessoaService>();
			services.AddScoped<IEspecialidadeService, EspecialidadeService>();
			#endregion

			return services;
		}
	}
}