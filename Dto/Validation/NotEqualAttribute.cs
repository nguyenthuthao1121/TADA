using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Validation
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonValue;

        public NotEqualAttribute(string comparisonValue)
        {
            _comparisonValue = comparisonValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonValue);
            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);
            if (value != null && otherValue.ToString() == _comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
