using HoraCerta.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HoraCerta.Controllers;

[Route("[controller]/[action]")]
public class EspecialidadeController(IEspecialidadeService especialidadeService) : Controller
{
	private readonly IEspecialidadeService _especialidadeService = especialidadeService;


	public async Task<IActionResult> Lista(int pagina = 1, int tamanhoPagina = 10)
	{
		var paginadas = await _especialidadeService.ObterPaginado(pagina, tamanhoPagina);
		return View(paginadas);
	}

}
