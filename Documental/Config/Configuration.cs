namespace Documental.Core.Config
{
    public class Configuration : IConfiguration
    {
        public string EndpointUri { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }

        public Configuration(string endpointUri, string key, string databaseName)
        {
            EndpointUri = endpointUri;
            Key = key;
            DatabaseName = databaseName;
        }
    }
}
