using FluentValidation;

namespace Uptec.Erp.Shared.Domain.ValueObjects
{
    public class Email : AbstractValidator<Email>
    {
        public const byte MaxLength = 100;

        public string EnderecoEmail { get; private set; }
        public bool EhValido => Validar();

        protected Email() { }

        public Email(string email)
        {
            EnderecoEmail = email;
        }

        private bool Validar()
        {
            RuleFor(e => e.EnderecoEmail)
                .MaximumLength(MaxLength)
                .EmailAddress();

            var validationResult = Validate(this);

            return validationResult.IsValid;
        }


    }
}