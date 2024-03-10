using System.Collections.Generic;
using System.Threading.Tasks;
using WPLauncher.Models;

namespace WPLauncher.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<AppProperties>> GetApplicationList();

        void UninstallApplication(AppProperties app);

        bool IsInstalled(AppProperties app);

        void ClearCache();
    }
}