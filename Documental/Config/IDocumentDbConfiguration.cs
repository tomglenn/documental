namespace Documental.Config
{
    public interface IDocumentDbConfiguration
    {
        string EndpointUri { get; set; }
        string Key { get; set; }
        string DatabaseName { get; set; }
    }
}
