using System.Text.RegularExpressions;

namespace HoraCerta.Domain.Helpers
{
	public static partial class RegexPatterns
	{
		/// <summary>
		/// Remove todos os caracteres não-numéricos
		/// </summary>
		[GeneratedRegex(@"\D")]
		public static partial Regex SomenteDigitos();

		/// <summary>
		/// Valida formato de CPF (000.000.000-00)
		/// </summary>
		[GeneratedRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")]
		public static partial Regex FormataCpf();

		/// <summary>
		/// Valida formato de CNPJ (00.000.000/0000-00)
		/// </summary>
		[GeneratedRegex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")]
		public static partial Regex FormataCnpj();

		/// <summary>
		/// Valida formato de telefone/celular
		/// </summary>
		[GeneratedRegex(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$")]
		public static partial Regex FormataTelefone();

		#region Validações de Formato

		public static bool ValidaFormatoCpf(string? valor) =>
		!string.IsNullOrWhiteSpace(valor) && FormataCpf().IsMatch(valor);

		public static bool ValidaFormatoCnpj(string? valor) =>
			!string.IsNullOrWhiteSpace(valor) && FormataCnpj().IsMatch(valor);

		public static bool ValidaFormatoTelefone(string? valor) =>
			!string.IsNullOrWhiteSpace(valor) && FormataTelefone().IsMatch(valor);

		public static string ValidaSeEhSomenteDigitos(string? valor) =>
			string.IsNullOrWhiteSpace(valor) ? string.Empty : SomenteDigitos().Replace(valor, "");

		#endregion
	}
}
