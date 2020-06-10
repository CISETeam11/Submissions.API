namespace Submissions.API.Models
{
    public class ModerationQueueMessage : QueueMessage
    {
        public Article Article { get; set; }
    }
}