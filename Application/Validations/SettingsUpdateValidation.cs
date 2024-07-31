using Application.DTOs.Input;
using FluentValidation;

namespace Application.Validations
{
    public class SettingsUpdateValidation : AbstractValidator<SettingsUpdateDTO>
    {
        public SettingsUpdateValidation()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
            RuleFor(p => p.IsVisibleStatus).NotEmpty().NotNull();
            RuleFor(p => p.IsVisibleMessageSeen).NotEmpty().NotNull();
            RuleFor(p => p.IsVisibleLastSeen).NotEmpty().NotNull();
        }
    }
}
