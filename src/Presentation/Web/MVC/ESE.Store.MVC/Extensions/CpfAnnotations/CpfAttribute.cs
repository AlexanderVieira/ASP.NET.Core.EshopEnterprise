using ESE.Core.DomainObjects;
using ESE.Store.MVC.Properties;
using System.ComponentModel.DataAnnotations;

namespace ESE.Store.MVC.Extensions.CpfAnnotations
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.CpfValid(value.ToString()) ? ValidationResult.Success : new ValidationResult(Resources.MSG_ERROR_CPF_INVALIDO);
        }
    }
}
