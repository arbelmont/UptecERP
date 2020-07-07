using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Events;
using Definitiva.Shared.Domain.Models;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Definitiva.Shared.Domain.Services
{
    public abstract class BaseService
    {
        private readonly IBus _bus;

        protected BaseService(IBus bus)
        {
            _bus = bus;
        }

        protected bool ValidateEntity<T>(T entity) where T : Entity<T>
        {
            if (entity == null)
            {
                NotifyError("Invalid null entity");
                return false;
            }

            if (entity.IsValid()) return true;

            NotifyDomainValidationErrors(entity.Validation.Result);
            NotifyDomainValidationErrors(entity.Validation.SystemResult);
            return false;
        }

        protected bool ValidateBusinessRules(ValidationResult validationResult)
        {
            if (validationResult.IsValid) return true;

            NotifyDomainValidationErrors(validationResult);
            return false;
        }

        protected void NotifyDomainValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _bus.PublishEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected void NotifyError(string mensagem)
        {
            _bus.PublishEvent(new DomainNotification("", mensagem));
        }
    }
}