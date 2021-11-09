using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPLauncher.Services
{
    public interface ILauncherApplicationService
    {
        Task<IEnumerable<AppProperties>> GetLauncherApplications();
    }
}