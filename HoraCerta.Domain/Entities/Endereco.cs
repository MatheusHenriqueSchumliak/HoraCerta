using HoraCerta.Domain.Entities.Base;

namespace HoraCerta.Domain.Entities
{
	public class Endereco : EntityBase
	{
		public Guid PessoaId { get; set; }
		public Pessoa? Pessoa { get; set; } = null;
		public string CEP { get; set; } = string.Empty;
		public string Rua { get; set; } = string.Empty;
		public string Numero { get; set; } = string.Empty;
		public string Bairro { get; set; } = string.Empty;
		public string Cidade { get; set; } = string.Empty;
		public string Estado { get; set; } = string.Empty;
		public string? Complemento { get; set; }

		public Endereco() { }

		public Endereco Criar(Guid pessoaId, string cep, string rua, string numero, string bairro, string cidade, string estado, string? complemento)
		{
			PessoaId = pessoaId;
			CEP = cep;
			Rua = rua;
			Numero = numero;
			Bairro = bairro;
			Cidade = cidade;
			Estado = estado;
			Complemento = complemento;
			return this;
		}

		public Endereco Atualizar(string cep, string rua, string numero, string bairro, string cidade, string estado, string? complemento)
		{
			CEP = cep;
			Rua = rua;
			Numero = numero;
			Bairro = bairro;
			Cidade = cidade;
			Estado = estado;
			Complemento = complemento;
			DataAtualizacao = DateTime.UtcNow;
			return this;
		}


	}
}
