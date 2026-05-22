using FluentValidation;

namespace Application.Modules.Commands.ConcluirModulo;

public class ConcluirModuloCommandValidator : AbstractValidator<ConcluirModuloCommand>
{
    public ConcluirModuloCommandValidator()
    {
        RuleFor(x => x.ProfissionalId)
            .NotEmpty().WithMessage("O ID do profissional é obrigatório.");

        RuleFor(x => x.ModuloId)
            .NotEmpty().WithMessage("O ID do módulo é obrigatório.");
    }
}