using System.Threading;
using System.Threading.Tasks;

namespace CleanConnect.Web.Init
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}