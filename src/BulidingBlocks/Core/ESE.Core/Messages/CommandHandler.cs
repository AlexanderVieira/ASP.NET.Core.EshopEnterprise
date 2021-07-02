using ESE.Core.Data.Interfaces;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace ESE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit()) AddError("Houve um erro ao persistir os dados.");
            return ValidationResult;
        }
    }
}
