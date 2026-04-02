using HoraCerta.Application.ViewModels.Prestador;

namespace HoraCerta.Application.ViewModels.Especialidade;

public class EspecialidadeViewModel
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string? Descricao { get; set; } = string.Empty;
	public DateTime DataCriacao { get; set; }
	public List<PrestadorViewModel> Prestadores { get; set; } = new List<PrestadorViewModel>();
	public string? Observacao { get; set; } = null;
}
