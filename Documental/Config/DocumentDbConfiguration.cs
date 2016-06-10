namespace Documental.Config
{
    public class DocumentDbConfiguration : IDocumentDbConfiguration
    {
        public string EndpointUri { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }

        public DocumentDbConfiguration(string endpointUri, string key, string databaseName)
        {
            EndpointUri = endpointUri;
            Key = key;
            DatabaseName = databaseName;
        }
    }
}
