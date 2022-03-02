using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IExamDal : IEntityRepository<Exam>
    {
        List<ExamDto> GetExamDetails();
        ExamDto GetExamDetail(int examId);
    }
}
