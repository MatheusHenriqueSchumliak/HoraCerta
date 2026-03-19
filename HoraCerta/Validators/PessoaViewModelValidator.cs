using HoraCerta.Application.ViewModels.Pessoa;
using System.Text.RegularExpressions;
using FluentValidation;

namespace HoraCerta.Application.Validators;

public class PessoaViewModelValidator : AbstractValidator<PessoaViewModel>
{
	public PessoaViewModelValidator()
	{
		RuleFor(p => p.Nome)
			.NotEmpty()
			.WithMessage("O nome é obrigatório.")
			.MaximumLength(100)
			.WithMessage("O nome deve ter no máximo 100 caracteres.");

		RuleFor(p => p.SobreNome)
			.NotEmpty()
			.WithMessage("O sobrenome é obrigatório.")
			.MaximumLength(100)
			.WithMessage("O sobrenome deve ter no máximo 100 caracteres.");

		RuleFor(p => p.Cpf)
			.NotEmpty()
			.WithMessage("O CPF é obrigatório.")
			.Must(cpf =>
			{
				if (string.IsNullOrWhiteSpace(cpf)) return false;
				var apenasDigitos = Regex.Replace(cpf, @"\D", "");
				return apenasDigitos.Length == 11;
			})
			.WithMessage("O CPF deve ter exatamente 11 caracteres.");

		RuleFor(p => p.Celular)
			.NotEmpty()
			.WithMessage("O celular é obrigatório.");

		RuleFor(p => p.DataNascimento)
			.NotEmpty()
			.WithMessage("A data de nascimento é obrigatória.")
			.LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser uma data passada.");
	}
}
