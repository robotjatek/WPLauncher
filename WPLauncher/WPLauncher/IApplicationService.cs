using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPLauncher
{
    public interface IApplicationService
    {
        Task<IEnumerable<AppProperties>> GetApplicationList();
    }
}