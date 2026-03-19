namespace HoraCerta.Application.ViewModels.Common;

public class PaginadorViewModel
{
	public int PageNumber { get; set; } = 1;
	public int TotalPages { get; set; } = 1;

	// Nome da action/handler a ser usado nos links. Em Razor Pages use null e construa URLs manualmente.
	public string? Action { get; set; }

	// Opcional: nome do controller (quando usar MVC)
	public string? Controller { get; set; }

	// Nome do parâmetro de rota/consulta para a página (padrao "page")
	public string PageParameterName { get; set; } = "page";

	// Classe CSS extra para o nav/pagination
	public string? CssClass { get; set; }
}
