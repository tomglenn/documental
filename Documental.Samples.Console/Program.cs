using System.Configuration;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Core;
using Documental.Samples.Console.Documents;

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

            var configuration = new DocumentDbConfiguration(endpointUri, key, databaseName);

            var creationStategy = new SampleDocumentDbCreationStrategy();
            await creationStategy.Create(configuration);
            
            var repository = new DocumentRepository(configuration);
            var person = new PersonDocument
            {
                FirstName = "Foo",
                LastName = "Glenn"
            };

            await repository.Save(person);

            System.Console.WriteLine("All done!");

            System.Console.ReadKey();
        }
    }
}
