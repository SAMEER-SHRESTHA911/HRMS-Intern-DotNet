using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace User.Domain.Common.DataAnnotations
{
    public class EmailAddressCustomValidation : ValidationAttribute
    {
        private readonly int _maxLength;

        public EmailAddressCustomValidation(int maxLength= 30)
        {
            _maxLength = maxLength;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("The email field is required.");
            }
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("The email field is required.");
            }
            if (email.Length > _maxLength)
            {
                return new ValidationResult($"The email must be at most {_maxLength} characters long.");
            }
            var emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (!emailRegex.IsMatch(email))
            {
                return new ValidationResult("The email address is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}
