using System.Threading.Tasks;

namespace VstsMetrics.Abstractions
{
    public interface IWorkItemClientFactory
    {
        Task<IWorkItemClient> Create(string projectCollectionUri, string patToken);
    }
}