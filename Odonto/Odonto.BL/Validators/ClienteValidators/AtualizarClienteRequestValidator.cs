using FluentValidation;
using Odonto.BL.DTO.ClienteDTO.Request;

namespace Odonto.BL.Validators.ClienteValidators;

public class AtualizarClienteRequestValidator : AbstractValidator<AtualizarClienteRequest>
{
    public AtualizarClienteRequestValidator()
    {
        When(x => x.Email is not null, () =>
            RuleFor(x => x.Email!)
                .EmailAddress()
                .WithMessage("E-mail inválido."));
 
        When(x => x.Telefone is not null, () =>
            RuleFor(x => x.Telefone!)
                .Matches(@"^\+?[\d\s\-()\[\]]{8,20}$")
                .WithMessage("Telefone inválido."));
 
        When(x => x.DataNascimento.HasValue, () =>
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
    
            RuleFor(x => x.DataNascimento!.Value)
                .LessThan(hoje)
                .WithMessage("Data de nascimento deve ser anterior à data atual.")
                .GreaterThan(hoje.AddYears(-150))
                .WithMessage("Data de nascimento inválida.");
        });
    }
}