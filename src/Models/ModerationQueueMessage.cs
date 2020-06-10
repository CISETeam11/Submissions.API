using System.ComponentModel.DataAnnotations;

namespace Submissions.API.Models
{
    public class ModerationQueueMessage : QueueMessage
    {
        [Required]
        public Article Article { get; set; }
    }
}