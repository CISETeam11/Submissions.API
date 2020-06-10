using System.ComponentModel.DataAnnotations;
using Submissions.API.Filters;

namespace Submissions.API.Models
{
    public class Article
    {
        [StringLength(1024)]
        public string Author { get; set; }

        [StringLength(1024)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Journal { get; set; }

        [RangeUntilCurrentYear(1800)]
        public int? Year { get; set; }

        public int? JournalIssue { get; set; }

        public int? Volume { get; set; }

        [StringLength(12)]
        public string Pages { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^10\.\d{4,9}\/[-.;()\/:\w]+$", ErrorMessage = "The field Doi does not match the DOI specification format.")]
        public string Doi { get; set; }
    }
}