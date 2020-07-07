using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;
using FluentValidation.Results;

namespace Definitiva.Shared.Domain.DomainValidator
{
    public abstract class DomainValidator<T> where T : Entity<T>
    {
        protected Dictionary<IDomainSpecification<T>, string> Rules { get; }

        protected DomainValidator()
        {
            Rules = new Dictionary<IDomainSpecification<T>, string>();
        }

        protected void Add(IDomainSpecification<T> rule, string message)
        {
            Rules.Add(rule, message);
        }

        public ValidationResult Validate(T obj)
        {
            foreach (var specification in Rules)
            {
                if (!specification.Key.IsSatisfiedBy(obj))
                    obj.Validation.Result.Errors.Add(new ValidationFailure("", specification.Value));
            }

            return obj.Validation.Result;
        }
    }
}