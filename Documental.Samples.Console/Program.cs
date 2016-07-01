using System;
using System.Configuration;
using System.Threading;
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
            var configuration = CreateConfiguration();
            var repository = new DocumentRepository(configuration);
            
            await CreateDatabase(configuration);

            await CreatePeople(repository);

            FindPersonCalledTom(repository);
            FindPeopleWithSurnameBar(repository);
            DeletePersonCalledJess(repository);
            FindPeopleWithSurnameBar(repository);

            System.Console.WriteLine("All done!");
            System.Console.ReadKey();
        }

        private static IDocumentDbConfiguration CreateConfiguration()
        {
            var endpointUri = ConfigurationManager.AppSettings["Documental:EndpointUri"];
            var key = ConfigurationManager.AppSettings["Documental:Key"];
            var databaseName = ConfigurationManager.AppSettings["Documental:DatabaseName"];

            return new DocumentDbConfiguration(endpointUri, key, databaseName);
        }

        private static async Task CreateDatabase(IDocumentDbConfiguration configuration)
        {
            var creationStategy = new SampleDocumentDbCreationStrategy();
            await creationStategy.Create(configuration);
        }

        private static async Task CreatePeople(IDocumentRepository repository)
        {
            await SavePerson(repository, "John", "Smith");
            await SavePerson(repository, "Tom", "Bar");
            await SavePerson(repository, "Jess", "Bar");
            await SavePerson(repository, "Foo", "Bar");
            System.Console.WriteLine("");
        }

        private static async Task SavePerson(IDocumentRepository repository, string firstName, string lastName)
        {
            var person = new PersonDocument
            {
                FirstName = firstName,
                LastName = lastName
            };

            await repository.Save(person);

            System.Console.WriteLine("Created person {0} {1}", firstName, lastName);
        }

        private static void FindPersonCalledTom(IDocumentRepository repository)
        {
            System.Console.WriteLine("Finding person called Tom");
            var person = repository.Query(new FindPersonByNameQuery("Tom"));
            if (person != null)
            {
                System.Console.WriteLine("Found {0} {1}", person.FirstName, person.LastName);
            }
            else
            {
                System.Console.WriteLine("Didn't find a person called Tom");
            }

            System.Console.WriteLine("");
        }

        private static void FindPeopleWithSurnameBar(IDocumentRepository repository)
        {
            System.Console.WriteLine("Finding people with surname Bar");
            var people = repository.Query(new FindPeopleBySurnameQuery("Bar"));
            if (people != null)
            {
                foreach (var p in people)
                {
                    System.Console.WriteLine("Found {0} {1}", p.FirstName, p.LastName);
                }
            }
            else
            {
                System.Console.WriteLine("Didn't find anyone with surname Bar");
            }

            System.Console.WriteLine("");
        }

        private static void DeletePersonCalledJess(IDocumentRepository repository)
        {
            System.Console.WriteLine("Deleting person called Jess");
            var jess = repository.Query(new FindPersonByNameQuery("Jess"));
            if (jess != null)
            {
                repository.Delete(jess);
                System.Console.WriteLine("Deleted Jess");
            }
            else
            {
                System.Console.WriteLine("Could not find a person called Jess");
            }

            System.Console.WriteLine("");
        }
    }
}
