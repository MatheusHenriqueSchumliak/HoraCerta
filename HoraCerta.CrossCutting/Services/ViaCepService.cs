using HoraCerta.CrossCutting.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HoraCerta.CrossCutting.Services;

public class ViaCepService : IViaCepService
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<ViaCepService>? _logger;

	// Construtor compatível com AddHttpClient: recebe HttpClient
	public ViaCepService(HttpClient httpClient, ILogger<ViaCepService>? logger = null)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_logger = logger;

		if (_httpClient.BaseAddress == null)
		{
			_httpClient.BaseAddress = new Uri("https://viacep.com.br/");
		}
	}

	public async Task<object?> ConsultarCepAsync(string cep)
	{
		if (string.IsNullOrWhiteSpace(cep)) return null;

		// Normaliza: mantém apenas dígitos
		var digits = new string(cep.Where(char.IsDigit).ToArray());
		if (digits.Length != 8) return null;

		try
		{
			// Desserializa diretamente para object usando HttpClientJsonExtensions
			var result = await _httpClient.GetFromJsonAsync<object>($"ws/{digits}/json/");
			return result;
		}
		catch (HttpRequestException ex)
		{
			_logger?.LogWarning(ex, "Erro na requisição ao ViaCep para {Cep}", cep);
			return null;
		}
		catch (JsonException ex)
		{
			_logger?.LogWarning(ex, "Erro ao desserializar resposta do ViaCep para {Cep}", cep);
			return null;
		}
	}
}