using System;
using FluentValidation;
using WebApi.Api.Dto;

namespace WebApi.Api.Validations
{
    public class SaveVersionValidator : AbstractValidator<UploadMaterialVersionDto>
    {
        public SaveVersionValidator()
        {
            RuleFor(m => m.Name)
               .NotEmpty();
        }
    }
}
