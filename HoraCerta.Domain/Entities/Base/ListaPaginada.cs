namespace HoraCerta.Domain.Entities.Base;

public class ListaPaginada<T>
{
	public List<T> Items { get; }
	public int NumeroPagina { get; }
	public int TamanhoPagina { get; }
	public int TotalItens { get; }
	public int TotalPaginas { get; }
	public bool TemAnterior => NumeroPagina > 1;
	public bool TemProximo => NumeroPagina < TotalPaginas;

	public ListaPaginada(List<T> items, int totalItens, int numeroPagina, int tamanhoPagina)
	{
		Items = items ?? new List<T>();
		TotalItens = totalItens;
		NumeroPagina = numeroPagina < 1 ? 1 : numeroPagina;
		TamanhoPagina = tamanhoPagina < 1 ? 10 : tamanhoPagina;
		TotalPaginas = (int)Math.Ceiling(TotalItens / (double)TamanhoPagina);
	}

}