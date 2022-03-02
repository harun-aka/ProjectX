using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfExamDal : EfEntityRepositoryBase<Exam, NorthwindContext>, IExamDal
    {
        public ExamDto GetExamDetail(int examId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = (from e   in context.Exams
                             join ar  in context.Articles on e.ArticleId equals ar.ArticleId
                             where e.ExamId == examId
                             select new ExamDto
                             {
                                 Article = new Article { ArticleId = ar.ArticleId, Text = ar.Text, Title = ar.Title },
                                 CreatedDate = e.CreatedDate,
                                 Questions = ( from  q in context.Questions
                                               where q.ExamId == e.ExamId
                                               select new QuestionDto
                                               {
                                                   QuestionId = q.QuestionId,
                                                   Text = q.Text,
                                                   Answers = ( from a in context.Answers
                                                               where a.QuestionId == q.QuestionId
                                                               select new Answer
                                                               {
                                                                   AnswerId = a.AnswerId,
                                                                   QuestionId = a.QuestionId,
                                                                   Text = a.Text
                                                               }).ToList()
                                               }).ToList()
                             });                                                 
                return result.Single();
            }
        }

    public List<ExamDto> GetExamDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = (from e in context.Exams
                              join ar in context.Articles on e.ArticleId equals ar.ArticleId
                              select new ExamDto
                              {
                                  Article = new Article { ArticleId = ar.ArticleId, Text = ar.Text, Title = ar.Title },
                                  CreatedDate = e.CreatedDate,
                                  Questions = (from q in context.Questions
                                               select new QuestionDto
                                               {
                                                   QuestionId = q.QuestionId,
                                                   Text = q.Text,
                                                   Answers = (from a in context.Answers
                                                              where a.QuestionId == q.QuestionId
                                                              select new Answer
                                                              {
                                                                  AnswerId = a.AnswerId,
                                                                  QuestionId = a.QuestionId,
                                                                  Text = a.Text
                                                              }).ToList()
                                               }).ToList()
                              });
                return result.ToList();
            }
        }
    }
}
