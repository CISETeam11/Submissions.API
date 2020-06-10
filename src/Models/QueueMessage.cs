using System.ComponentModel.DataAnnotations;
using Submissions.API.Contracts;

namespace Submissions.API.Models
{
    public class QueueMessage : IQueueMessage
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string PopReceipt { get; set; }
    }
}