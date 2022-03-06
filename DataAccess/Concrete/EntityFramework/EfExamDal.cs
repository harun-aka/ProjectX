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

    public class EfExamDal : EfEntityRepositoryBase<Exam, ExamContext>, IExamDal
    {
        public ExamDto GetExamDetail(int examId)
        {
            using (ExamContext context = new ExamContext())
            {
                var result = (from e   in context.Exams
                             join ar  in context.Articles on e.ArticleId equals ar.ArticleId
                             where e.ExamId == examId
                             select new ExamDto
                             {
                                 ExamId = e.ExamId,
                                 Article = new Article { ArticleId = ar.ArticleId, Text = ar.Text, Title = ar.Title },
                                 ExamCreationDate = e.CreationDate,
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

    public List<ExamListDto> GetExamListDetails()
        {
            using (ExamContext context = new ExamContext())
            {
                var result = (from e in context.Exams
                              join ar in context.Articles on e.ArticleId equals ar.ArticleId
                              select new ExamListDto
                              {
                                  ExamId = e.ExamId,
                                  ArticleText = ar.Text, 
                                  ArticleTitle = ar.Title,
                                  ExamCreationDate = e.CreationDate,
                              });

                return result.ToList();
            }
        }
    }
}
