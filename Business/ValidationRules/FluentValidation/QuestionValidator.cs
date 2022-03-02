using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Text).NotNull();
            RuleFor(q => q.Text).NotEmpty();
        }
    }
}
