using System.Collections.Generic;
using System.Threading.Tasks;
using WPLauncher.Models;

namespace WPLauncher.Services
{
    public interface ILauncherApplicationService
    {
        Task<IEnumerable<AppProperties>> GetLauncherApplications();
    }
}