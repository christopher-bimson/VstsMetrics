namespace VstsMetrics.Abstractions
{
    public interface IWorkItemClientFactory
    {
        IWorkItemClient Create(string projectCollectionUri, string patToken);
    }
}