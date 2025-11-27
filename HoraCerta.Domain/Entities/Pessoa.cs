using HoraCerta.Domain.Entities.Base;
using HoraCerta.Domain.Enumerables;

namespace HoraCerta.Domain.Entities
{
	public class Pessoa : EntityBase
	{
		public string Nome { get; set; } = string.Empty;
		public string SobreNome { get; set; } = string.Empty;
		public string? Telefone { get; set; }
		public string Celular { get; set; } = string.Empty;
		public DateTime DataNascimento { get; set; }
		public string Cpf { get; set; } = string.Empty;
		public virtual Endereco Endereco { get; set; } 
		public StatusEnum Status { get; set; } = StatusEnum.Ativo;

		public Pessoa() { }

		public Pessoa Criar(string nome, string sobreNome, string? telefone, string celular, DateTime dataNascimento, string cpf, Guid enderecoId, Endereco endereco, StatusEnum status)
		{
			Nome = nome;
			SobreNome = sobreNome;
			Telefone = telefone;
			Celular = celular;
			DataNascimento = dataNascimento;
			Cpf = cpf;
			Endereco = endereco;
			Status = status;

			return this;
		}

		public Pessoa Atualizar(string nome, string sobreNome, string? telefone, string celular, DateTime dataNascimento, string cpf, StatusEnum status)
		{
			Nome = nome;
			SobreNome = sobreNome;
			Telefone = telefone;
			Celular = celular;
			DataNascimento = dataNascimento;
			Cpf = cpf;
			Status = status;
			DataAtualizacao = DateTime.UtcNow;
			return this;
		}

		public void AlterarStatus(StatusEnum status)
		{
			Status = status;
			DataAtualizacao = DateTime.UtcNow;
		}

	}
}
