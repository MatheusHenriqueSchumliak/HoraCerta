using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HoraCerta.CrossCutting.Interfaces;
using HoraCerta.CrossCutting.Services;

namespace HoraCerta.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCrossCuttingServices(this IServiceCollection services, IConfiguration configuration)
	{

		#region CrossCutting
		services.AddHttpClient<IViaCepService, ViaCepService>();
		#endregion

		return services;
	}
}