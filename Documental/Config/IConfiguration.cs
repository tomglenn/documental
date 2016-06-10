namespace Documental.Core.Config
{
    public interface IConfiguration
    {
        string EndpointUri { get; set; }
        string Key { get; set; }
        string DatabaseName { get; set; }
    }
}
