using FluentValidation.Results;

namespace Definitiva.Shared.Domain.Validations
{
    public class Validation
    {
        public ValidationResult Result { get; private set; }
        public ValidationResult SystemResult { get; private set; }

        public Validation(ValidationResult result)
        {
            Result = result;
            SystemResult = new ValidationResult();
        }

        public Validation(ValidationResult result, ValidationResult systemResult)
        {
            Result = result;
            SystemResult = systemResult;
        }

        public bool IsValid()
        {
            if (Result == null || SystemResult == null)
                return false;

            return Result.IsValid && SystemResult.IsValid;
        }
    }
}
