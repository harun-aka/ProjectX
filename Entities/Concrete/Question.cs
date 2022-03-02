using Core.Entities;

namespace Entities.Concrete
{
    public class Question : IEntity
    {
        public int QuestionId { get; set; } 
        public string Text { get; set; }
        public int ExamId { get; set; }
    }
}
