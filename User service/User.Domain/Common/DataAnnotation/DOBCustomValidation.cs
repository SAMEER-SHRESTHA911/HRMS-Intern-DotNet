using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Common.DataAnnotation
{
    public class DOBCustomValidation : ValidationAttribute
    {
        private readonly int _minimumAge;

        public DOBCustomValidation(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date of birth is required.");
            }

            if (value is string dobString)
            {
                if (DateTime.TryParse(dobString, out DateTime dob))
                {
                    var age = DateTime.Today.Year - dob.Year;

                    if (dob > DateTime.Today.AddYears(-age))
                    {
                        age--;
                    }
                    
                    if (age < _minimumAge)
                    {
                        return new ValidationResult($"You must be at least {_minimumAge} years old.");
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid date format.");
                }
            }


            return new ValidationResult("Invalid input type.");
        }
    }
}
