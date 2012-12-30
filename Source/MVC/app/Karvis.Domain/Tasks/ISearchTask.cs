using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
    public interface ISearchTask
    {
        SearchViewModel GetRawModel();
        SearchResultViewModel Search(SearchViewModel searchViewModel, string sort, string sortdir, int page);
        JobViewModel GetJobsByTagName(string name, string sort, string sortdir, int page);
    }
}   