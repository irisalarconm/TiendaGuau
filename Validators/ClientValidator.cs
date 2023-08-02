using FluentValidation;
using TiendaGuau.Models;

namespace TiendaGuau.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator() 
        { 
            RuleFor(client => client.NameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.NameClient).MaximumLength(150).WithMessage("This field is can not has more than 150 characters");

            RuleFor(client => client.LastnameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.LastnameClient).MaximumLength(150).WithMessage("This field is can not has more than 150 characters");

            RuleFor(client => client.DNIClient).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();

            RuleFor(client => client.AdressClient).NotEmpty().WithMessage("This field is required.");

            RuleFor(client => client.Phone).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();
        }
    }
}
