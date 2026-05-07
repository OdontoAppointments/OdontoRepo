using FluentValidation;
using Odonto.BL.DTO.ClienteDTO.Request;

namespace Odonto.BL.Validators.ClienteValidators;

public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
{
    public CriarClienteRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .MaximumLength(150)
            .WithMessage("Nome deve ter no máximo 150 caracteres.");
 
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-mail é obrigatório.")   
            .EmailAddress()
            .WithMessage("E-mail inválido.");
 
        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("Telefone é obrigatório.")
            .Matches(@"^\+?[\d\s\-()\[\]]{8,20}$")
            .WithMessage("Telefone inválido.");
 
        RuleFor(x => x.DataNascimento)
            .NotEmpty()
            .WithMessage("Data de nascimento é obrigatória.")
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Data de nascimento deve ser anterior à data atual.")
            .GreaterThan(DateOnly.FromDateTime(DateTime.Today).AddYears(-150))
            .WithMessage("Data de nascimento inválida.");
 
        RuleFor(x => x.Endereco)
            .NotNull()
            .WithMessage("Endereço é obrigatório.")
            .SetValidator(new EnderecoRequestValidator());
    }
}