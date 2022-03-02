using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ExamDto : IDto
    {
        public int ExamId { get; set; }
        public DateTime ExamCreationDate { get; set; }
        public Article Article { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
