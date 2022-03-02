using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExamManager : IExamService
    {
        IExamDal _examDal;
        IAnswerService _answerService;
        IArticleService _articleService;
        IQuestionService _questionService;

        public ExamManager(IExamDal examDal, IAnswerService answerService, IArticleService articleService, IQuestionService questionService)
        {
            _examDal = examDal;
            _answerService = answerService;
            _articleService = articleService;
            _questionService = questionService;
        }

        [TransactionScopeAspect]

        public IResult Delete(int id)// Soruları ve cevapları da sil
        {
            Exam exam = _examDal.Get(e => e.ExamId == id);
            if (exam is null)
            {
                return new ErrorResult(Messages.ExamNotFound);
            }

            _examDal.Delete(exam);
            return new SuccessResult(Messages.ExamDeleted);
        }

        [TransactionScopeAspect]
        public IResult Add(Exam exam)
        {
            _examDal.Add(exam);
            return new SuccessResult(Messages.ExamAdded);
        }

        public IDataResult<ExamDto> Get(int id)
        {
            return new SuccessDataResult<ExamDto>(_examDal.GetExamDetail(id), Messages.ExamListed);
        }

        public IDataResult<Answer> GetAnswer(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ExamListDto>> GetAll()
        {
            return new SuccessDataResult<List<ExamListDto>>(_examDal.GetExamListDetails(), Messages.ExamsListed);
        }

        [TransactionScopeAspect]
        public IResult SaveExam(ExamDto examDto)
        {
            IResult result = BusinessRules.Run(CheckAllQuestionsCreated(examDto));

            if (result != null)
            {
                throw new Exception(result.Message);
            }

            _articleService.Add(examDto.Article);

            Exam exam = new Exam
            {
                CreationDate = DateTime.Now,
                ArticleId = examDto.Article.ArticleId,
            };
            Add(exam);
            foreach (var questionDto in examDto.Questions)
            {
                Question question = new Question
                {
                    ExamId = exam.ExamId,
                    Text = questionDto.Text
                };

                _questionService.Add(question);

                    foreach (var answer in questionDto.Answers)
                {
                    answer.QuestionId = question.QuestionId;
                    _answerService.Add(answer);
                }
            }

            return new SuccessResult(Messages.SaveExam);
        }

        private static IResult CheckAllQuestionsCreated(ExamDto exam)
        {
            if (exam.Questions.Count != 4)
            {
                return new ErrorResult(Messages.QuestionCountNotFour);
            }
            return new SuccessResult();
        }
    }
}
