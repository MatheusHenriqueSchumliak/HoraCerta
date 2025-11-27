using HoraCerta.Application.ViewModels.Endereco;

namespace HoraCerta.Application.ViewModels.Pessoa;

public class PessoaViewModel
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string SobreNome { get; set; } = string.Empty;
	public string? Telefone { get; set; } = null;
	public string Celular { get; set; } = string.Empty;
	public DateTime DataNascimento { get; set; }
	public DateTime DataCadastro { get; set; }
	public string Cpf { get; set; } = string.Empty;
	public EnderecoViewModel Endereco { get; set; } = new EnderecoViewModel();
}
