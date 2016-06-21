# Documental
A slick and easy to use Azure DocumentDB helper Library

## Installation
`Install-Package Documental`

## Creating a `Document`
To create a Document that can be used with Documental you must inherit from `Microsoft.Azure.Documents.Document` and decorate your class with the `Documental.Attributes.DocumentTypeAttribute` as shown below.

```
[DocumentType("people")]
public class PersonDocument : Microsoft.Azure.Documents.Document
{
  [JsonProperty(PropertyName = "firstName")]
  public string FirstName { get; set; }

  [JsonProperty(PropertyName = "lastName")]
  public string LastName { get; set; }

  public PersonDocument()
  {
    Id = Guid.NewGuid().ToString();
  }
}
```

Alternatively you can inherit from `Documental.Core.AbstractDocument` which simply subclasses `Microsoft.Azure.Documents.Document` and sets the `Id` property to `Guid.NewGuid().ToString()` in the constructor. However this is not necessary and is merely a shortcut for setting a default unique `Id`.

## Creating the `IDocumentDbConfiguration` Object
To configure Documental you must first create an instance of an `IDocumentDbConfiguration` object. This object is used by both `DocumentDbCreationStrategy` (explained below) and the `DocumentRepository`. You can do this however you like, an example is shown below.

```
private static IDocumentDbConfiguration CreateConfiguration()
{
    var endpointUri = ConfigurationManager.AppSettings["Documental:EndpointUri"];
    var key = ConfigurationManager.AppSettings["Documental:Key"];
    var databaseName = ConfigurationManager.AppSettings["Documental:DatabaseName"];

    return new DocumentDbConfiguration(endpointUri, key, databaseName);
}
```

## Using the `DocumentRepository`
Documental uses the repository pattern to interface with your DocumentDB instance. This allows you to easily perform saves, deletes and queries without having to use the verbose underlying DocumentDB library.

To begin you must first create an instance of the `DocumentRepository` as shown below.

```
var repository = new DocumentRepository(configuration);
```

### Basic Operations
#### Finding a Document by Id
```
var person = await repository.FindById("<The Person's Id>");
```

#### Saving/Updating a Document
```
var person = new PersonDocument("John", "Smith");
await repository.Save(person);
```

#### Deleting a Document
```
await repository.Delete(person);
```

### Querying Documents
Documental allows you to query documents in various ways.

The easiest way to query documents is by using a predicate directly as shown below.
```
var firstPersonCalledTom = repository.FirstOrDefault<PersonDocument>(x => x.FirstName == "Tom");
var somePeopleCalledTom = repository.Where<PersonDocument>(x => x.FirstName == "Tom").Skip(1).Take(2);
```

Should you wish to bring more structure to your code base, you can make use of the `SingleDocumentQuery` and `MultipleDocumentQuery` objects as shown below.

#### Using the `SingleDocumentQuery`
You can inherit from `SingleDocumentQuery` in order to write a structured query that returns a single Document.

```
public class FindPersonByNameQuery : SingleDocumentQuery<PersonDocument>
{
    private readonly string name;

    public FindPersonByNameQuery(string name)
    {
        this.name = name;
    }

    protected override PersonDocument ExecuteQuery(IOrderedQueryable<PersonDocument> query)
    {
        return query.Where(x => x.FirstName == name).AsEnumerable().FirstOrDefault();
    }
}
```

*Note: The DocumentDB LINQ provider does not appear to currently support predicates in **FirstOrDefault()** hence the use of **Where** and **AsEnumerable()***

And then used as below.

```
var personCalledTom = repository.Query(new FindPersonByNameQuery("Tom"));
```

#### Using the `MultipleDocumentQuery`
You can inherit from `MultipleDocumentQuery` in order to write a structured query that returns multiple Documents.

```
public class FindPeopleBySurnameQuery : MultipleDocumentQuery<PersonDocument>
{
    private readonly string surname;

    public FindPeopleBySurnameQuery(string surname)
    {
        this.surname = surname;
    }

    protected override IEnumerable<PersonDocument> ExecuteQuery(IOrderedQueryable<PersonDocument> query)
    {
        return query.Where(x => x.LastName == surname);
    }
}
```

And then used as below.

```
var peopleWithSurnameBar = repository.Query(new FindPeopleBySurnameQuery("Bar"));
```

*Note that queries do not use the **await** keyword*

## Creating a `Database`
**Note: If you do not wish to programatically generate your database and document collections you can skip this step.**

In order to start saving documents to your Azure DocumentDB instance, you must first create a database and create the document collections that you wish to store inside it. You can do this directly from the Azure Portal if you wish, or you can do it in code as shown below.

To do this, Documental has a concept of a `DocumentDbCreationStrategy`. A `DocumentDbCreationStrategy` allows you to define how your database is created, what collections get created and allows you to hook into the pre/during/post steps of creation to perform whatever custom logic you need to using `DocumentDbCreationStrategyContributor` objects.

To begin, you must create your own by inheriting from `DocumentDbCreationStrategy`. An example of a custom creation strategy is shown below.

```
public class SampleDocumentDbCreationStrategy : DocumentDbCreationStrategy
{
    public SampleDocumentDbCreationStrategy()
    {
        AddContributor(new DeleteDatabaseIfExistsContributor());
        AddContributor(new FromAttributesMappingContributor<PersonDocument>());
    }
}
```

You can then create an instance of this `DocumentDbCreationStrategy` and call the `Create` method on it to generate your database.

```
var configuration = CreateConfiguration();
var creationStategy = new SampleDocumentDbCreationStrategy();
await creationStategy.Create(configuration);
```

*You may notice the use of the **await** keyword. The DocumentDB library makes use of C#'s asynchronous abilities and as such Documental follows suit.*

#### Explaining `DocumentDbCreationStrategyContributor` Objects
A `DocumentDbCreationStrategyContributor` allows you to define what actions to take pre/during and post database creation. This can be extremely useful for example if you want to delete an existing database first or perhaps seed your database post creation.

There are two contributors already built for you.

The first is `DeleteDatabaseIfExistsContributor` which first checks to see if a database exists with the same name as set in your `IDocumentDbConfiguration` object. If it does, it deletes it and then proceeds to create the new database - very useful during development when you want to purge test data each time.

The second is `FromAttributesMappingContributor` which will create a Document Collection for the specified Document based on its `DocumentTypeAttribute` values.

#### Creating your own `DocumentDbCreationStrategyContributor`
Should you wish to hook into the Database creation process at all this can be achieved by implementing your own `DocumentDbCreationStrategyContributor`.
There are a few different methods that you can override, these are as follows:

```
Task OnPreCreate(DocumentClient client, IDocumentDbConfiguration configuration);
```
This runs before the Database is created. Ideally this is where you would do any pre-creation checks, archive or delete the old database etc (see `DeleteDatabaseIfExistsContributor`).

```
Task Contribute(DocumentClient client, IDocumentDbConfiguration configuration);
```
This runs after the Database has been created. Ideally this is where you would create Document Collections (see `FromAttributesMappingContributor`).

```
Task OnPostCreate(DocumentClient client, IDocumentDbConfiguration configuration);
```
This runs after the Database has been created and after all `Contribute` methods have been ran. Ideally this is where you would run any data seeding.

## Bugs / Issues / Suggestions?
Thanks for checking out Documental. If you've found any bugs or issues, or perhaps you'd like to make a suggestion then please raise a GitHub issue or create a Pull Request. :)
