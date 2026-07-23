using HoraCerta.Application.Interfaces.IServices;
using HoraCerta.Application.ViewModels.Endereco;
using HoraCerta.Application.ViewModels.Pessoa;
using HoraCerta.CrossCutting.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HoraCerta.Controllers;

[Route("[controller]/[action]")]
public class PessoaController(IPessoaService pessoaService, IViaCepService viaCepService) : Controller
{
	private readonly IPessoaService _pessoaService = pessoaService;
	private readonly IViaCepService _viaCepService = viaCepService;

	[HttpGet("{cep}")]
	public async Task<IActionResult> ConsultarCep(string cep)
	{
		// ... validação do cep ...
		var result = await _viaCepService.ConsultarCepAsync(cep);
		if (result == null) return NotFound(new { erro = true, mensagem = "CEP não encontrado" });
		return Ok(result);
	}

	// GET: Pessoas
	public async Task<IActionResult> Lista(int pagina = 1, int tamanhoPagina = 10)
	{
		var paginadas = await _pessoaService.ObterPaginado(pagina, tamanhoPagina);
		// Garante que TotalPaginas esteja corretamente calculado se a service não fez

		return View(paginadas);
	}

	// GET: Pessoas/Exibir/5
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Exibir(Guid id)
	{
		var pessoa = await _pessoaService.ObterPorId(id);
		if (pessoa == null) return NotFound();
		return View(pessoa);
	}

	// GET: Pessoas/Create
	public IActionResult Criar()
	{
		var model = new PessoaViewModel
		{
			Nome = string.Empty,
			SobreNome = string.Empty,
			Telefone = string.Empty,
			Celular = string.Empty,
			DataNascimento = DateTime.Today,
			DataCadastro = DateTime.Now,
			Cpf = string.Empty,
			Observacao = string.Empty,
			EhProfissional = false,
			Endereco = new EnderecoViewModel
			{
				CEP = string.Empty,
				Rua = string.Empty,
				Numero = string.Empty,
				Bairro = string.Empty,
				Cidade = string.Empty,
				Estado = string.Empty,
				Complemento = string.Empty
			}
		};

		return View(model);
	}

	// POST: Pessoas/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Criar(PessoaViewModel model)
	{
		if (!ModelState.IsValid) return View(model);

		var resultado = await _pessoaService.Criar(model);

		if (!resultado.Sucesso)
		{
			foreach (var erro in resultado.Erros)
			{
				ModelState.AddModelError(string.Empty, erro);
			}
			// Junta todos os erros em uma string separada por quebra de linha
			TempData["AlertaErro"] = string.Join("<br/>", resultado.Erros);

			return View(model);
		}

		TempData["AlertaSucesso"] = resultado.Mensagem ?? "Cadastro realizado!";
		return RedirectToAction(nameof(Lista));
	}

	// GET: Pessoas/Editar/5
	public async Task<IActionResult> Editar(Guid id)
	{
		var pessoa = await _pessoaService.ObterPorId(id);
		if (pessoa == null) return NotFound();
		return View(pessoa);
	}

	// POST: Pessoas/Editar/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Editar(Guid id, PessoaViewModel model)
	{
		if (!ModelState.IsValid) return View(model);
		await _pessoaService.Atualizar(id, model);
		return RedirectToAction(nameof(Exibir), new { id = id });
	}

	//// GET: Pessoas/Delete/5
	//public async Task<IActionResult> Delete(Guid id)
	//{
	//	var pessoa = await _pessoaService.ObterPorId(id);
	//	if (pessoa == null) return NotFound();
	//	return View(pessoa);
	//}

	//// POST: Pessoas/Delete/5
	//[HttpPost, ActionName("Delete")]
	//[ValidateAntiForgeryToken]
	//public async Task<IActionResult> DeleteConfirmed(Guid id)
	//{
	//	await _pessoaService.Remover(id);
	//	return RedirectToAction(nameof(Index));
	//}
}
