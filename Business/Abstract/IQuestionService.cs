using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        IResult Add(Question question);
        IDataResult<List<Question>> GetByExamId(int examId);
    }
}
