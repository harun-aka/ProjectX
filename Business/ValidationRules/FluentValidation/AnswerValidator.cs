using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Text).NotEmpty();
            RuleFor(a => a.QuestionId).NotEmpty();
        }
    }
}
