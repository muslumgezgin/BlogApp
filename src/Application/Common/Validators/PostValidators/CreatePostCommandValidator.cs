using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Features.PostFeatures.Commands.Create;
using FluentValidation;

namespace Blog.Application.Common.Validators.PostValidators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{Title} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Content)
            .NotEmpty().WithMessage("{Content} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{Content} must not exceed 50 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{Description} is required.")
            .NotNull()
            .MaximumLength(350).WithMessage("{Description} must not exceed 350 characters.");

        RuleFor(p => p.AuthorId)
            .NotEmpty().WithMessage("{AuthorId} is required.")
            .NotNull();
    }
    
}