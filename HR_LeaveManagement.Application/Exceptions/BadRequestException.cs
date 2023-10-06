using FluentValidation.Results;

namespace HR_LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, ValidationResult result) : base(message)
        {
            foreach (var error in result.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
        }

        public List<string> ValidationErrors { get; set; }
    }
}
