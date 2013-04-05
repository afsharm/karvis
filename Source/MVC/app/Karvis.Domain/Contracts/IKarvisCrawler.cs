using System.IO;

namespace Karvis.Domain.JobExtract
{
    public interface IKarvisCrawler
    {
        string ExtractEmailsByText(ref string content);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
    }
}
