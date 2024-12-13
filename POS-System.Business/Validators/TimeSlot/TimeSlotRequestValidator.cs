using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.TimeSlot
{
    public class TimeSlotRequestValidator : AbstractValidator<TimeSlotRequest>
    {
        public TimeSlotRequestValidator()
        {
            RuleFor(x => x.IsAvailable)
                .NotEmpty()
                .WithMessage("Availability for time slot must be set.");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time for time slot is required.")
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Time slot start time must start later than the current date.");
        }
    }
}
