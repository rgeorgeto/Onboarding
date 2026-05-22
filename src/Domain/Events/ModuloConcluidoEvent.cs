using Domain.Entities;

namespace Domain.Events;

public class ModuloConcluidoEvent
{
    public Guid ProfissionalId { get; }
    public Modulo Modulo { get; }

    public ModuloConcluidoEvent(Guid profissionalId, Modulo modulo)
    {
        ProfissionalId = profissionalId;
        Modulo = modulo;
    }
}