using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAnswerService
    {
        IResult Add(Answer answer);
        IDataResult<Answer> Get(int answerId);
    }
}
