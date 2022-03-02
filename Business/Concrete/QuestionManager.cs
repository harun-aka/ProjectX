using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [TransactionScopeAspect]
        public IResult Add(Question question)
        {
            _questionDal.Add(question);
            return new SuccessResult(Messages.QuestionAdded);
        }

        public IDataResult<List<Question>> GetByExamId(int examId)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetAll(a => a.ExamId == examId), Messages.QuestionsListed);
        }
    }
}
