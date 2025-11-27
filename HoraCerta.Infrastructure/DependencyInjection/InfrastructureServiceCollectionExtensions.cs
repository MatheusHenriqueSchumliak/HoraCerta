using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HoraCerta.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

			return services;
		}
	}
}