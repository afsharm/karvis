using System;
using System.IO;

namespace Karvis.Core
{
    public interface IKarvisCrawler
    {
        string ExtractEmailsByText(ref string content);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
    }
}
