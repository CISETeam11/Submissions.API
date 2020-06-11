using System.ComponentModel.DataAnnotations;
using Submissions.API.Contracts;
using Submissions.API.Filters;

namespace Submissions.API.Models
{
    public class SubmissionArticle : IArticle
    {
        [StringLength(1024, MinimumLength = 3)]
        public string Author { get; set; }

        [StringLength(1024, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255, MinimumLength = 3)]
        public string Journal { get; set; }

        [RangeUntilCurrentYear(1900)]
        public int? Year { get; set; }

        [Range(0, int.MaxValue)]
        public int? JournalIssue { get; set; }

        [Range(0, int.MaxValue)]
        public int? Volume { get; set; }

        [StringLength(12, MinimumLength = 3)]
        public string Pages { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^10\.\d{4,9}\/[-.;()\/:\w]+$", ErrorMessage = "The field Doi does not match the DOI specification format.")]
        public string Doi { get; set; }
    }
}