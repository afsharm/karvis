using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
    public interface IAdminTask 
    {
        AdminViewModel GetRawModel();
        AdminExtractJobResultViewModel ExtractJobs(AdSource targetSource);
    }
}
