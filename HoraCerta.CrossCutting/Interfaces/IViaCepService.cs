namespace HoraCerta.CrossCutting.Interfaces;

public interface IViaCepService
{
	Task<object?> ConsultarCepAsync(string cep);
}


