using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TiendaGuau.Models;

namespace TiendaGuau.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        TiendaGuauContext _context;
        public ClientValidator(TiendaGuauContext context) 
        { 
            _context = context;


            RuleFor(client => client.NameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.NameClient).MaximumLength(75).WithMessage("This field is can not has more than 150 characters");

            RuleFor(client => client.LastnameClient).NotEmpty().WithMessage("This field is required");
            RuleFor(client => client.LastnameClient).MaximumLength(75).WithMessage("This field is can not has more than 150 characters");


            RuleFor(client => client.DNIClient).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();
            RuleFor(client => client.DNIClient).Must(BeUnique).WithMessage("This DNI already exists");

            RuleFor(client => client.AdressClient).NotEmpty().WithMessage("This field is required.");

            RuleFor(client => client.Phone).GreaterThan(0).WithMessage("This field must be greater than 0.").NotEmpty();
        }

        private bool BeUnique(Client client, long DNI)
        {
            if (client.ClientId == 0)
            {
                return !_context.Client.Any(p => p.DNIClient == client.DNIClient);
            }

            return !_context.Client.Any(c => c.DNIClient == client.DNIClient && c.ClientId != client.ClientId);
            
        }
    }
}
