using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
    public interface ISearchTask
    {
        SearchViewModel GetRawModel();
        object Search(SearchViewModel searchViewModel);
    }
}