using Core.Entities;

namespace Entities.DTOs
{
    public class ExamListDto : IDto
    {
        public int ExamId { get; set; }
        public DateTime ExamCreationDate { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleText { get; set; }
    }
}
