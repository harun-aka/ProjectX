using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
