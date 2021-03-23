using System;
using FluentValidation;
using WebApi.Api.Dto;

namespace WebApi.Api.Validations
{
    public class SaveMaterialValidator : AbstractValidator<UploadMaterialDto>
    {
        public SaveMaterialValidator()
        {
            RuleFor(m => m.Name)
               .NotEmpty()
               .MaximumLength(50);
            RuleFor(m => m.CategoryNameId)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(2);
        }
    }
}
