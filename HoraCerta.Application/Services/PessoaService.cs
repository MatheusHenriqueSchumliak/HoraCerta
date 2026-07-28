using HoraCerta.Application.Interfaces.IRepository;
using HoraCerta.Application.Interfaces.IServices;
using HoraCerta.Application.ViewModels.Common;
using HoraCerta.Application.ViewModels.Pessoa;
using HoraCerta.Application.Factorys;
using HoraCerta.Domain.Entities.Base;
using HoraCerta.CrossCutting.Helpers;

namespace HoraCerta.Application.Services;

public class PessoaService(IPessoaRepository pessoaRepository) : IPessoaService
{
	#region Construtor
	private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
	#endregion Construtor

	#region C.R.U.D.
	public async Task<IEnumerable<PessoaViewModel>> ObterTodos()
	{
		var pessoas = await _pessoaRepository.ObterTodos().ConfigureAwait(false);
		return pessoas.Select(PessoaFactory.ParaViewModel);
	}

	public async Task<PessoaViewModel?> ObterPorId(Guid id)
	{
		var pessoa = await _pessoaRepository.ObterPorId(id).ConfigureAwait(false);
		return pessoa is null ? null : PessoaFactory.ParaViewModel(pessoa);
	}

	public async Task<ResultadoOperacao> Criar(PessoaViewModel model)
	{
		// Limpa o CPF para conter apenas números
		model.Cpf = RegexPatterns.ValidaSeEhSomenteDigitos(model.Cpf);

		// Limpa Telefone e Celular para conter apenas números
		model.Telefone = RegexPatterns.ValidaSeEhSomenteDigitos(model.Telefone);
		model.Celular = RegexPatterns.ValidaSeEhSomenteDigitos(model.Celular);

		var existente = await _pessoaRepository.ObterPorCpf(model.Cpf);
		if (existente != null)
			return ResultadoOperacao.Falha("CPF já cadastrado.");

		// Cria entidade a partir do ViewModel usando a factory
		var pessoa = PessoaFactory.Criar(model);

		await _pessoaRepository.Adicionar(pessoa).ConfigureAwait(false);
		await _pessoaRepository.Salvar().ConfigureAwait(false);

		return ResultadoOperacao.Ok("Pessoa cadastrada com sucesso!");
	}

	public async Task Atualizar(Guid id, PessoaViewModel model)
	{
		// Busca a entidade existente
		var existente = await _pessoaRepository.ObterPorId(id).ConfigureAwait(false);
		if (existente is null) throw new InvalidOperationException("Pessoa não encontrada.");

		// Limpa o CPF para conter apenas números
		model.Cpf = RegexPatterns.ValidaSeEhSomenteDigitos(model.Cpf);

		// Limpa Telefone e Celular para conter apenas números
		model.Telefone = RegexPatterns.ValidaSeEhSomenteDigitos(model.Telefone);
		model.Celular = RegexPatterns.ValidaSeEhSomenteDigitos(model.Celular);

		// Atualiza a entidade existente usando a factory
		PessoaFactory.Atualizar(existente, model);

		_pessoaRepository.Atualizar(existente);
		await _pessoaRepository.Salvar().ConfigureAwait(false);
	}

	public async Task Remover(Guid id)
	{
		var pessoa = await _pessoaRepository.ObterPorId(id).ConfigureAwait(false);
		if (pessoa is null) throw new InvalidOperationException("Pessoa não encontrada.");

		_pessoaRepository.Remover(pessoa);
		await _pessoaRepository.Salvar().ConfigureAwait(false);
	}

	#endregion C.R.U.D.

	#region Personalizados

	#endregion

	#region Paginação
	public async Task<ListaPaginada<PessoaViewModel>> ObterPaginado(int pagina, int tamanhoPagina)
	{
		var paginado = await _pessoaRepository.ObterPaginado(pagina, tamanhoPagina).ConfigureAwait(false);
		var itensViewModel = paginado.Items.Select(PessoaFactory.ParaViewModel).ToList();
		return new ListaPaginada<PessoaViewModel>(itensViewModel, paginado.TotalItens, paginado.NumeroPagina, paginado.TamanhoPagina);
	}
	#endregion
}
