using FluentValidation;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Validators.CartItem;

namespace POS_System.Business.Validators.Employee
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(EmployeeValidationMessages.UserNameRequired);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(\+[\d]{1,4})?\d{5,14}$")
                .WithMessage(EmployeeValidationMessages.InvalidPhoneNumber);

            RuleFor(x => x.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-EmployeeValidationConstants.MinimumAge)))
                .WithMessage(EmployeeValidationMessages.AgeRestriction);
        }
    }
}
