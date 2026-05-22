using Domain.Entities;

namespace Domain.Events;

public class ConquistaDesbloqueadaEvent
{
    public Guid ProfissionalId { get; }
    public Conquista Conquista { get; }

    public ConquistaDesbloqueadaEvent(Guid profissionalId, Conquista conquista)
    {
        ProfissionalId = profissionalId;
        Conquista = conquista;
    }
}