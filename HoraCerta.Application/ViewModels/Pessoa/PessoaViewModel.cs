using HoraCerta.Application.ViewModels.Endereco;
using System.Text.RegularExpressions;

namespace HoraCerta.Application.ViewModels.Pessoa;

public class PessoaViewModel
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string SobreNome { get; set; } = string.Empty;
	public string? Telefone { get; set; } = null;
	public string Celular { get; set; } = string.Empty;
	public DateTime DataNascimento { get; set; }
	public DateTime DataCadastro { get; set; }
	public string Cpf { get; set; } = string.Empty;
	public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();
	public string? Observacao { get; set; } = null;

	public string CpfFormatado
	{
		get
		{
			var digits = SomenteDigitos(Cpf);
			if (digits.Length != 11)
				return Cpf; // retorna como está se não for um CPF válido

			// Exibe os 3 primeiros e os 2 últimos dígitos, oculta o meio
			return $"{digits.Substring(0, 3)}.***.***-{digits.Substring(9, 2)}";
		}
	}
	public string CelularFormatado
	{
		get
		{
			var digits = SomenteDigitos(Celular);
			if (digits.Length == 11)
				return Convert.ToUInt64(digits).ToString(@"\(00\) 00000\-0000");
			if (digits.Length == 10)
				return Convert.ToUInt64(digits).ToString(@"\(00\) 0000\-0000");
			return Celular;
		}
	}

	private static string SomenteDigitos(string input)
		=> string.IsNullOrEmpty(input) ? "" : Regex.Replace(input, @"\D", "");
}
