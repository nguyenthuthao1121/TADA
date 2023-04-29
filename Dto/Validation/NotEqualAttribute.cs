using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TADA.Dto.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NotEqualAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = Convert.ToString(value);
            if (!password.Equals("19122003"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Password is wrong");
            }
        }
    }
}
