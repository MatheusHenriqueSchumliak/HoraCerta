using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Application.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;
using HoraCerta.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using HoraCerta.CrossCutting.Interfaces;
using HoraCerta.CrossCutting.Services;
using HoraCerta.Application.Services;

namespace HoraCerta.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
	{
		//Registro de dependências:
		#region Application Services
		//services.AddScoped<IProfissionalService, ProfissionalService>();
		services.AddScoped<IPessoaService, PessoaService>();
		services.AddScoped<IEspecialidadeService, EspecialidadeService>();
		#endregion

		#region Data Repositories
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IPessoaRepository, PessoaRepository>();
		services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
		//services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
		#endregion

		#region CrossCutting
		services.AddHttpClient<IViaCepService, ViaCepService>();
		#endregion

		// outros serviços, handlers, clients, etc.

		return services;
	}
}