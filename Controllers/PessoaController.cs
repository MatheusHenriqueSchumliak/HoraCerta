using HoraCerta.Application.Interfaces.IServices;
using HoraCerta.Application.ViewModels.Pessoa;
using Microsoft.AspNetCore.Mvc;

namespace HoraCerta.Controllers;

public class PessoaController : Controller
{
	private readonly IPessoaService _pessoaService;

	public PessoaController(IPessoaService pessoaService)
	{
		_pessoaService = pessoaService;
	}

	// GET: Pessoas
	public async Task<IActionResult> Lista(int pagina = 1, int tamanhoPagina = 10)
	{
		var paginadas = await _pessoaService.ObterPaginado(pagina, tamanhoPagina);
		return View(paginadas);
	}

	//// GET: Pessoas/Details/5
	//public async Task<IActionResult> Details(Guid id)
	//{
	//	var pessoa = await _pessoaService.ObterPorId(id);
	//	if (pessoa == null) return NotFound();
	//	return View(pessoa);
	//}

	// GET: Pessoas/Create
	public IActionResult Criar()
	{
		return View();
	}

	// POST: Pessoas/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Criar(PessoaViewModel model)
	{
		if (!ModelState.IsValid) return View(model);
		await _pessoaService.Criar(model);
		return RedirectToAction(nameof(Index));
	}

	//// GET: Pessoas/Edit/5
	//public async Task<IActionResult> Edit(Guid id)
	//{
	//	var pessoa = await _pessoaService.ObterPorId(id);
	//	if (pessoa == null) return NotFound();
	//	return View(pessoa);
	//}

	//// POST: Pessoas/Edit/5
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public async Task<IActionResult> Edit(Guid id, PessoaViewModel model)
	//{
	//	if (!ModelState.IsValid) return View(model);
	//	await _pessoaService.Atualizar(id, model);
	//	return RedirectToAction(nameof(Index));
	//}

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
