using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class CommentUpdateRequestValidator : AbstractValidator<CommentUpdateRequest>
{
    public CommentUpdateRequestValidator()
    {
        RuleFor(x => x.Content)
           .NotEmpty().WithMessage("Seu comentário deve ter algo!")
           .When(x => x.Stars == 0);

        RuleFor(x => x.Stars)
            .NotEmpty().WithMessage("Seu comentário deve ter algo!")
            .Must(x => x < 0 || x > 5).WithMessage("Número de estrelas devem estar entre 0 e 5")
            .When(x => x.Content.Length == 0);
    }
}