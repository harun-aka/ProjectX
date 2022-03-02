using Core.Entities;

namespace Entities.Concrete
{
    public class Exam : IEntity
    {
        public int ExamId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ArticleId { get; set; }
    }
}
