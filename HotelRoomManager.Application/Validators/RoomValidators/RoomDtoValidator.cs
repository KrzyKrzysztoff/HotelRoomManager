using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Models;

namespace HotelRoomManager.Application.Validators.RoomValidator
{
    public class RoomDtoValidator : AbstractValidator<RoomDto>
    {
        public RoomDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Room name is required.")
                .MaximumLength(100)
                .WithMessage("Room name cannot be longer than 100 characters.");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Room size must be greater than 0!");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid room status.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot be longer than 500 characters.");

            When(x => x.Status is RoomStatus.ManuallyLocked or RoomStatus.Maintenance, () =>
            {
                RuleFor(x => x.Detail)
                    .NotNull().WithMessage("Details are required for ManuallyLocked or Maintenance.");

                RuleFor(x => x.Detail!.Reason)
                    .NotEmpty().WithMessage("Reason is required for ManuallyLocked or Maintenance.")
                    .MaximumLength(100)
                    .WithMessage("Reason cannot be longer than 100 characters.");
            });

            When(x => x.Status is RoomStatus.Cleaning or RoomStatus.Occupied, () =>
            {
                RuleFor(x => x.Detail!.Reason)
                    .NotEmpty().WithMessage("Reason cannot be empty if details are provided.")
                    .When(x => x.Detail != null)  
                    .MaximumLength(500)
                    .WithMessage("Reason cannot be longer than 500 characters.");
            });
        }
    }
}