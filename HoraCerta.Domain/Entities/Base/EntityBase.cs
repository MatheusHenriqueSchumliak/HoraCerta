namespace HoraCerta.Domain.Entities.Base;

public class EntityBase
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
	public DateTime? DataAtualizacao { get; set; } = null;
	public EntityBase()
	{
		Id = Guid.NewGuid();
		DataCriacao = DateTime.UtcNow;
		DataAtualizacao = null;
	}
}
