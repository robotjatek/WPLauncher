using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPLauncher.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<AppProperties>> GetApplicationList();
    }
}