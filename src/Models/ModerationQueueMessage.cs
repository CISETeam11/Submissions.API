﻿using System.ComponentModel.DataAnnotations;

namespace Submissions.API.Models
{
    public class ModerationQueueMessage : QueueMessage
    {
        [Required]
        public SubmissionArticle Article { get; set; }
    }
}