namespace HoraCerta.Application.ViewModels.Common
{
	public class ResultadoOperacao
	{
		public bool Sucesso { get; set; }
		public string? Mensagem { get; set; }
		public List<string> Erros { get; set; } = new();

		public static ResultadoOperacao Ok(string? mensagem = null)
		{
			return new() { Sucesso = true, Mensagem = mensagem };
		}

		public static ResultadoOperacao Falha(params string[] erros)
		{
			return new() { Sucesso = false, Erros = [.. erros], Mensagem = erros.FirstOrDefault() };
		}
	}

	public class ResultadoOperacao<T> : ResultadoOperacao
	{
		public T? Dados { get; set; }

		public static ResultadoOperacao<T> Ok(T dados, string? mensagem = null)
		{
			return new() { Sucesso = true, Dados = dados, Mensagem = mensagem };
		}

		public static new ResultadoOperacao<T> Falha(params string[] erros)
		{
			return new() { Sucesso = false, Erros = [.. erros], Mensagem = erros.FirstOrDefault() };
		}
	}
}