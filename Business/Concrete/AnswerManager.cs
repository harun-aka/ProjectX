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
    public class AnswerManager : IAnswerService
    {
        IAnswerDal _answerDal;

        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }

        [ValidationAspect(typeof(AnswerValidator))]
        [TransactionScopeAspect]
        public IResult Add(Answer answer)
        {
            _answerDal.Add(answer);

            return new SuccessResult(Messages.AnswerAdded);
        }

        public IDataResult<Answer> Get(int answerId)
        {
            return new SuccessDataResult<Answer>(_answerDal.Get(a => a.AnswerId == answerId));
        }
    }
}
