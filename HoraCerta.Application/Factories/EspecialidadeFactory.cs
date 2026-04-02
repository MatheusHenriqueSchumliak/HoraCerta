using HoraCerta.Application.ViewModels.Especialidade;
using HoraCerta.Domain.Entities;

namespace HoraCerta.Application.Factories;

public static class EspecialidadeFactory
{
	#region Entidade para ViewModel
	public static EspecialidadeViewModel ParaViewModel(Especialidade especialidade)
	{
		return new EspecialidadeViewModel
		{
			Id = especialidade.Id,
			Nome = especialidade.Nome,
			Descricao = especialidade.Descricao,
			DataCriacao = especialidade.DataCriacao

		};
	}
	#endregion

	#region ViewModel para entidade
	public static Especialidade Criar(EspecialidadeViewModel model)
	{
		return new Especialidade
		{
			Id = model.Id,
			Nome = model.Nome,
			Descricao = model.Descricao,
			DataCriacao = model.DataCriacao
		};
	}

	public static Especialidade Atualizar(Especialidade existente, EspecialidadeViewModel model)
	{
		return new Especialidade
		{

			Id = existente.Id,
			Nome = existente.Nome,
			Descricao = existente.Descricao,
			DataAtualizacao = DateTime.UtcNow
		};
	}
	#endregion

}
