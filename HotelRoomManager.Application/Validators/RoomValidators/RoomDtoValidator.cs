using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HotelRoomManager.Application.DTOs;

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
        }
    }
}