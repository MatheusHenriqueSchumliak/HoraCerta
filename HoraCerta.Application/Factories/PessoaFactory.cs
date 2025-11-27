using HoraCerta.Application.ViewModels.Endereco;
using HoraCerta.Application.ViewModels.Pessoa;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Application.Factorys;

public static class PessoaFactory
{
	#region Entidade para ViewModel
	public static PessoaViewModel ParaViewModel(Pessoa pessoa)
	{
		return new PessoaViewModel
		{
			Nome = pessoa.Nome,
			SobreNome = pessoa.SobreNome,
			Telefone = pessoa.Telefone,
			Celular = pessoa.Celular,
			DataNascimento = pessoa.DataNascimento,
			Cpf = pessoa.Cpf,
			Endereco = new EnderecoViewModel
			{
				CEP = pessoa.Endereco?.CEP ?? string.Empty,
				Rua = pessoa.Endereco?.Rua ?? string.Empty,
				Numero = pessoa.Endereco?.Numero ?? string.Empty,
				Bairro = pessoa.Endereco?.Bairro ?? string.Empty,
				Cidade = pessoa.Endereco?.Cidade ?? string.Empty,
				Estado = pessoa.Endereco?.Estado ?? string.Empty,
				Complemento = pessoa.Endereco?.Complemento
			},
			DataCadastro = pessoa.DataCriacao,
		};
	}

	#endregion

	#region ViewModel para entidade
	public static Pessoa Criar(PessoaViewModel model)
	{
		return new Pessoa
		{
			Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
			Nome = model.Nome,
			SobreNome = model.SobreNome,
			Telefone = model.Telefone,
			Celular = model.Celular,
			DataNascimento = model.DataNascimento,
			Cpf = model.Cpf,
			Endereco = new Endereco
			{
				CEP = model.Endereco.CEP,
				Rua = model.Endereco.Rua,
				Numero = model.Endereco.Numero,
				Bairro = model.Endereco.Bairro,
				Cidade = model.Endereco.Cidade,
				Estado = model.Endereco.Estado,
				Complemento = model.Endereco.Complemento
			}
		};
	}

	public static Pessoa Atualizar(Pessoa existente, PessoaViewModel model)
	{
		return new Pessoa
		{
			Id = existente.Id,
			Nome = model.Nome,
			SobreNome = model.SobreNome,
			Telefone = model.Telefone,
			Celular = model.Celular,
			DataNascimento = model.DataNascimento,
			Cpf = model.Cpf,
			Endereco = existente.Endereco,
			DataCriacao = existente.DataCriacao,
			DataAtualizacao = DateTime.UtcNow,
			Status = existente.Status
		};

	}

	#endregion

}
