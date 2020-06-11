using System.ComponentModel.DataAnnotations;

namespace Submissions.API.Models
{
    public class AnalysisQueueMessage : QueueMessage
    {
        [Required]
        public ModerationArticle Article { get; set; }
    }
}