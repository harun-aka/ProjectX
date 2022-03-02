using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Title).NotNull();
            RuleFor(a => a.Text).NotNull();
            RuleFor(a => a.Text).NotEmpty();
        }
    }
}
