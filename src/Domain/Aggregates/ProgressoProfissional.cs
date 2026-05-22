using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Aggregates;

public class ProgressoProfissional
{
    public Guid Id { get; private set; }
    public Guid ProfissionalId { get; private set; }
    public DateTime DataAdmissao { get; private set; }

    private readonly List<ProgressoModulo> _modulos = new();
    public IReadOnlyList<ProgressoModulo> Modulos => _modulos;

    private readonly List<Guid> _conquistasDesbloqueadas = new();
    public IReadOnlyList<Guid> ConquistasDesbloqueadas => _conquistasDesbloqueadas;

    private readonly List<object> _domainEvents = new();
    public IReadOnlyList<object> DomainEvents => _domainEvents;

    private ProgressoProfissional() { }

    public ProgressoProfissional(Guid profissionalId, DateTime dataAdmissao, List<Modulo> modulos)
    {
        Id = Guid.NewGuid();
        ProfissionalId = profissionalId;
        DataAdmissao = dataAdmissao;

        foreach (var modulo in modulos.OrderBy(m => m.Ordem))
        {
            var status = modulo.EhInformativo
                ? StatusModulo.Liberado
                : StatusModulo.Bloqueado;

            _modulos.Add(new ProgressoModulo(modulo.Id, status));
        }
    }

    public void LiberarModulo(Guid moduloId)
    {
        var progresso = _modulos.FirstOrDefault(m => m.ModuloId == moduloId);
        progresso?.Liberar();
    }

    public (bool sucesso, string? erro, ConquistaDesbloqueadaEvent? conquistaEvent)
        ConcluirModulo(Guid moduloId, Modulo modulo, List<Modulo> todosModulos, List<Conquista> conquistas)
    {
        var progresso = _modulos.FirstOrDefault(m => m.ModuloId == moduloId);
        if (progresso is null)
            return (false, "Módulo não encontrado no progresso do profissional.", null);

        if (progresso.Status != StatusModulo.Liberado)
            return (false, $"Módulo não está disponível para conclusão. Status atual: {progresso.Status}", null);

        progresso.Concluir();

        var conquistaDesbloqueada = VerificarConquistas(todosModulos, conquistas);
        if (conquistaDesbloqueada is not null)
        {
            _conquistasDesbloqueadas.Add(conquistaDesbloqueada.Id);
            var evento = new ConquistaDesbloqueadaEvent(ProfissionalId, conquistaDesbloqueada);
            _domainEvents.Add(evento);
            return (true, null, evento);
        }

        _domainEvents.Add(new ModuloConcluidoEvent(ProfissionalId, modulo));
        return (true, null, null);
    }

    private Conquista? VerificarConquistas(List<Modulo> todosModulos, List<Conquista> conquistas)
    {
        var modulosConcluidos = _modulos
            .Where(m => m.Status == StatusModulo.Concluido)
            .Select(m => todosModulos.First(t => t.Id == m.ModuloId))
            .ToList();

        foreach (var conquista in conquistas)
        {
            if (_conquistasDesbloqueadas.Contains(conquista.Id))
                continue;

            if (VerificarRegraConquista(conquista, modulosConcluidos, todosModulos))
                return conquista;
        }

        return null;
    }

    private static bool VerificarRegraConquista(
        Conquista conquista,
        List<Modulo> modulosConcluidos,
        List<Modulo> todosModulos)
    {
        return conquista.Regra switch
        {
            "M1" => modulosConcluidos.Any(m => m.Ordem == 1),
            "M1-M4" => modulosConcluidos.Count(m => m.Ordem >= 1 && m.Ordem <= 4) >= 4,
            "M5-M8" => modulosConcluidos.Count(m => m.Ordem >= 5 && m.Ordem <= 8) >= 4,
            "M9" => modulosConcluidos.Any(m => m.Ordem == 9),
            "M10" => modulosConcluidos.Any(m => m.Ordem == 10),
            "M11" => modulosConcluidos.Any(m => m.Ordem == 11),
            "Todos" => modulosConcluidos.Count >= todosModulos.Count(m => m.Ativo),
            _ => false
        };
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}