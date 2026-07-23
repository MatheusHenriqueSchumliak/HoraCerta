using HoraCerta.Application.ViewModels.Endereco;
using HoraCerta.Application.ViewModels.Pessoa;
using HoraCerta.Domain.Entities;
using HoraCerta.Domain.Enumerables;

namespace HoraCerta.Application.Factorys;

public static class PessoaFactory
{
	#region Entidade para ViewModel
	public static PessoaViewModel ParaViewModel(Pessoa pessoa)
	{
		return new PessoaViewModel
		{
			Id = pessoa.Id,
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
			Observacao = pessoa.Observacao,
		};
	}

	#endregion

	#region ViewModel para entidade
	public static Pessoa Criar(PessoaViewModel model)
	{
		var pessoaId = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;

		var endereco = new Endereco().Criar(
			pessoaId: pessoaId,
			cep: model.Endereco.CEP,
			rua: model.Endereco.Rua,
			numero: model.Endereco.Numero,
			bairro: model.Endereco.Bairro,
			cidade: model.Endereco.Cidade,
			estado: model.Endereco.Estado,
			complemento: model.Endereco.Complemento
		);

		var pessoa = new Pessoa().Criar(
			nome: model.Nome,
			sobreNome: model.SobreNome,
			telefone: model.Telefone,
			celular: model.Celular,
			dataNascimento: model.DataNascimento,
			cpf: model.Cpf,
			observacao: model.Observacao ?? string.Empty,
			endereco: endereco,
			status: StatusCadastro.Ativo
		);

		pessoa.Id = pessoaId;

		return pessoa;
	}

	public static void Atualizar(Pessoa existente, PessoaViewModel model)
	{
		// Usa o método de domínio da entidade Pessoa
		existente.Atualizar(
			nome: model.Nome,
			sobreNome: model.SobreNome,
			telefone: model.Telefone,
			celular: model.Celular,
			dataNascimento: model.DataNascimento,
			cpf: model.Cpf,
			observacao: model.Observacao ?? string.Empty,
			status: existente.Status

		);

		// Usa o método de domínio da entidade Endereco
		if (existente.Endereco != null)
		{
			existente.Endereco.Atualizar(
				cep: model.Endereco.CEP,
				rua: model.Endereco.Rua,
				numero: model.Endereco.Numero,
				bairro: model.Endereco.Bairro,
				cidade: model.Endereco.Cidade,
				estado: model.Endereco.Estado,
				complemento: model.Endereco.Complemento
			);
		}

	}

	#endregion

}
