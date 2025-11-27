using HoraCerta.Application.ViewModels.Endereco;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Application.Factories;

public static class EnderecoFactory
{
	#region Entidade para ViewModel
	public static Endereco Criar(EnderecoViewModel model)
	{
		return new Endereco
		{
			CEP = model.CEP,
			Rua = model.Rua,
			Numero = model.Numero,
			Bairro = model.Bairro,
			Cidade = model.Cidade,
			Estado = model.Estado,
			Complemento = model.Complemento
		};
	}

	#endregion

	#region ViewModel para entidade
	public static EnderecoViewModel ParaViewModel(Endereco endereco)
	{
		return new EnderecoViewModel
		{
			CEP = endereco.CEP,
			Rua = endereco.Rua,
			Numero = endereco.Numero,
			Bairro = endereco.Bairro,
			Cidade = endereco.Cidade,
			Estado = endereco.Estado,
			Complemento = endereco.Complemento
		};
	}
	
	#endregion

}
