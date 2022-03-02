using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExamService
    {
        IResult SaveExam(ExamDto exam);
        IDataResult<List<ExamDto>> GetAll();
        IResult Delete(int id);
        IDataResult<ExamDto> Get(int id);
        IDataResult<Answer> GetAnswer(int id);
    }
}
