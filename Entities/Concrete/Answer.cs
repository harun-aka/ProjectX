using Core.Entities;

namespace Entities.Concrete
{
    public class Answer : IEntity
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool? IsRight { get; set; }
        public int QuestionId { get; set; }
    }
}
