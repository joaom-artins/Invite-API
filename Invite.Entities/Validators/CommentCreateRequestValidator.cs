using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class CommentCreateRequestValidator : AbstractValidator<CommentCreateRequest>
{
    public CommentCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Seu comentário deve ter algo!")
            .When(x => x.Stars == 0);

        RuleFor(x => x.Stars)
            .InclusiveBetween(0, 5).WithMessage("Número de estrelas devem estar entre 0 e 5")
            .When(x => x.Content.Length == 0);
    }
}
