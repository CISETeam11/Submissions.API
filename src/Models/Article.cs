using System.ComponentModel.DataAnnotations;

namespace Submissions.API.Models
{
    public class Article
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Journal { get; set; }

        public int Year { get; set; }

        public int? JournalIssue { get; set; }

        public int? Volume { get; set; }

        public string Pages { get; set; }

        [Required]
        public string Doi { get; set; }
    }
}