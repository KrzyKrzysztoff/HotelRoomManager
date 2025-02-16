using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Models;
using HotelRoomManager.Application.DTOs;

namespace HotelRoomManager.Application.Validators.RoomValidators
{
    public class UpdateRoomAvailabilityDtoValidator : AbstractValidator<UpdateRoomAvailabilityDto>
    {
        public UpdateRoomAvailabilityDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid room status.");

            // Required details for ManuallyLocked and Maintenance
            When(x => x.Status is RoomStatus.ManuallyLocked or RoomStatus.Maintenance, () =>
            {
                RuleFor(x => x.Detail)
                    .NotNull().WithMessage("Details are required for ManuallyLocked or Maintenance.");

                RuleFor(x => x.Detail!.Reason)
                    .NotEmpty().WithMessage("Reason is required for ManuallyLocked or Maintenance.");
            });

            // Optional details for Cleaning and Occupied
            When(x => x.Status == RoomStatus.Cleaning || x.Status == RoomStatus.Occupied, () =>
            {
                RuleFor(x => x.Detail)
                    .NotNull().WithMessage("Details are optional, but must be valid if provided.");

                RuleFor(x => x.Detail!.Reason)
                    .NotEmpty().WithMessage("Reason cannot be empty if details are provided.")
                    .When(x => x.Detail is not null);
            });
        }
    }
}