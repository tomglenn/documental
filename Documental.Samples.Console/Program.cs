using System.Configuration;
using System.Threading.Tasks;
using Documental.Config;
using Documental.Core;
using Documental.Samples.Console.Documents;
using Documental.Samples.Console.Queries;

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

            await SavePerson(repository, "John", "Smith");
            await SavePerson(repository, "Tom", "Bar");
            await SavePerson(repository, "Jess", "Bar");
            await SavePerson(repository, "Foo", "Bar");

            System.Console.WriteLine("Saved 3 people");

            System.Console.WriteLine("Finding person called Tom");
            var person = repository.Query(new PersonCalledTomQuery());
            if (person != null)
            {
                System.Console.WriteLine("Found {0} {1}", person.FirstName, person.LastName);
            }
            else
            {
                System.Console.WriteLine("Didn't find a person called Tom");
            }

            System.Console.WriteLine("Finding people with surname Glenn");
            var people = repository.Query(new PeopleWithSurnameBarQuery());
            if (people != null)
            {
                foreach (var p in people)
                {
                    System.Console.WriteLine("Found {0} {1}", p.FirstName, p.LastName);
                }
            }
            else
            {
                System.Console.WriteLine("Didn't find anyone with surname Glenn");
            }

            System.Console.WriteLine("All done!");

            System.Console.ReadKey();
        }

        private static async Task SavePerson(DocumentRepository repository, string firstName, string lastName)
        {
            var person = new PersonDocument
            {
                FirstName = firstName,
                LastName = lastName
            };

            await repository.Save(person);
        }
    }
}
