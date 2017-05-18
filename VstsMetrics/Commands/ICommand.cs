using System.Threading.Tasks;

namespace VstsMetrics.Commands
{
    public interface ICommand
    {
        Task Execute();
    }
}