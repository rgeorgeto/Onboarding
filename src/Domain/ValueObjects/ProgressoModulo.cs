using Domain.Enums;

namespace Domain.ValueObjects;

public class ProgressoModulo
{
    public Guid ModuloId { get; private set; }
    public StatusModulo Status { get; private set; }
    public DateTime? DataConclusao { get; private set; }

    public ProgressoModulo(Guid moduloId, StatusModulo status)
    {
        ModuloId = moduloId;
        Status = status;
    }

    public void Concluir()
    {
        Status = StatusModulo.Concluido;
        DataConclusao = DateTime.UtcNow;
    }

    public void Liberar()
    {
        if (Status == StatusModulo.Bloqueado)
            Status = StatusModulo.Liberado;
    }
}