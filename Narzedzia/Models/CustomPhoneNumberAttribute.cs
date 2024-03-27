using System.ComponentModel.DataAnnotations;

public class CustomPhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string phoneNumber = value.ToString();

            // Sprawdź, czy numer telefonu to nie powtarzające się cyfry (np. 000000000, 111111111, itp.)
            if (!phoneNumber.Distinct().Skip(1).Any())
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}
