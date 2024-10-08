﻿using Application.DTOs.Input;
using FluentValidation;

namespace Application.Validations
{
    public class UserUpdateValidaton : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidaton()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(p => p.Name).NotNull().NotEmpty().Length(5, 100);
            RuleFor(p => p.NickName).NotNull().NotEmpty().Length(2, 45);
            RuleFor(p => p.Photo).NotNull().NotEmpty();
            RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(p => p.Password).NotNull().NotEmpty().Length(8, 100);
        }
    }
}