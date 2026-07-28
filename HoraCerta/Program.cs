using HoraCerta.Infrastructure.DependencyInjection;
using HoraCerta.CrossCutting.DependencyInjection;
using HoraCerta.Application.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace HoraCerta;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Desabilita a geração implícita de data-val-required para non-nullable reference types
		builder.Services.AddControllersWithViews(options =>
		{
			options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
		});

		// Substitua o uso obsoleto por AddValidatorsFromAssemblyContaining
		builder.Services.AddValidatorsFromAssemblyContaining<PessoaViewModelValidator>();

		builder.Services.AddHttpClient();


		// Habilitar auto-validation e adapters client-side (opcional, mas recomendado)
		builder.Services.AddFluentValidationAutoValidation();
		builder.Services.AddFluentValidationClientsideAdapters();

		// Adiciona injeção de dependência da infrastructure (DbContext + Repositories + Services)
		builder.Services.AddInfrastructureServices(builder.Configuration);

		// Adiciona injeção de dependência do CrossCutting (ViaCep, etc)
		builder.Services.AddCrossCuttingServices(builder.Configuration);

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}
