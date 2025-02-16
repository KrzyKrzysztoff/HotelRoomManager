using FluentValidation;
using HotelRoomManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Application.Validators.RoomValidators
{
    public class AvailabilityDetailDtoValidator : AbstractValidator<AvailabilityDetailDto>
    {
        public AvailabilityDetailDtoValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Reason is required.")
                .MaximumLength(100)
                .WithMessage("Reason cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot be longer than 500 characters.");
        }
    }
}
