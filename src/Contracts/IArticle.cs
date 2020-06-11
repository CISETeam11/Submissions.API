namespace Submissions.API.Contracts
{
    public interface IArticle
    {
        string Author { get; set; }

        string Title { get; set; }

        string Journal { get; set; }

        int? Year { get; set; }

        int? JournalIssue { get; set; }

        int? Volume { get; set; }

        string Pages { get; set; }

        string Doi { get; set; }
    }
}