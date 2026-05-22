using FluentValidation;

namespace Application.Modules.Commands.IniciarOnboarding;

public class IniciarOnboardingCommandValidator : AbstractValidator<IniciarOnboardingCommand>
{
    public IniciarOnboardingCommandValidator()
    {
        RuleFor(x => x.ProfissionalId)
            .NotEmpty().WithMessage("O ID do profissional é obrigatório.");

        RuleFor(x => x.DataAdmissao)
            .NotEmpty().WithMessage("A data de admissão é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de admissão não pode ser futura.");
    }
}