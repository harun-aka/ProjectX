using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IExamDal : IEntityRepository<Exam>
    {
        List<ExamListDto> GetExamListDetails();
        ExamDto GetExamDetail(int examId);
    }
}
