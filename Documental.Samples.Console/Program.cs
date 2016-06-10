using System.Configuration;
using System.Threading.Tasks;
using Configuration = Documental.Core.Config.Configuration;

namespace Documental.Samples.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var endpointUri = ConfigurationManager.AppSettings["Documental:EndpointUri"];
            var key = ConfigurationManager.AppSettings["Documental:Key"];
            var databaseName = ConfigurationManager.AppSettings["Documental:DatabaseName"];

            var configuration = new Configuration(endpointUri, key, databaseName);

            var creationStategy = new CreationStrategy();
            await creationStategy.Create(configuration);

            System.Console.WriteLine("Created database '{0}'", configuration.DatabaseName);
            System.Console.ReadKey();
        }
    }
}
