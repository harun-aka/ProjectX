using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        [ValidationAspect(typeof(ArticleValidator))]
        [TransactionScopeAspect]
        public IResult Add(Article article)
        {
            _articleDal.Add(article);
            return new SuccessResult(Messages.ArticleAdded);
        }

        public IDataResult<Article> GetArticle(int id)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(a => a.ArticleId == id), Messages.ArticleListed);
        }
    }
}
