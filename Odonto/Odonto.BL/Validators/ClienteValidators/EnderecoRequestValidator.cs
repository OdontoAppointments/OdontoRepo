using Odonto.BL.DTO.ClienteDTO.Request;

namespace Odonto.BL.Validators.ClienteValidators;
using FluentValidation;

public class EnderecoRequestValidator : AbstractValidator<EnderecoRequest>
{
    public EnderecoRequestValidator()
    {
        RuleFor(x => x.Rua)
            .NotEmpty().WithMessage("Rua é obrigatória.")
            .MaximumLength(200).WithMessage("Rua deve ter no máximo 200 caracteres.");
 
        RuleFor(x => x.Numero)
            .NotEmpty()
            .WithMessage("Número é obrigatório.")
            .ExclusiveBetween(0, 99999999)
            .WithMessage("Número inválido."); 
        
        RuleFor(x => x.Bairro)
            .NotEmpty()
            .WithMessage("Bairro é obrigatório.")
            .MaximumLength(100)
            .WithMessage("Bairro deve ter no máximo 100 caracteres.");
 
        RuleFor(x => x.CEP)
            .NotEmpty()
            .WithMessage("CEP é obrigatório.")
            .Matches(@"^\d{5}-?\d{3}$")
            .WithMessage("CEP inválido. Formato esperado: 00000-000.");
 
        RuleFor(x => x.Estado)
            .NotEmpty()
            .WithMessage("Estado é obrigatório.")
            .Length(2)
            .WithMessage("Estado deve conter a sigla de 2 letras (ex: SP).");
 
        RuleFor(x => x.Cidade)
            .NotEmpty()
            .WithMessage("Cidade é obrigatória.")
            .MaximumLength(100)
            .WithMessage("Cidade deve ter no máximo 100 caracteres.");
    }
}