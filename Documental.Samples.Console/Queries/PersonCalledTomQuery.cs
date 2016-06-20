using System.Linq;
using Documental.Queries;
using Documental.Samples.Console.Documents;

namespace Documental.Samples.Console.Queries
{
    public class PersonCalledTomQuery : SingleDocumentQuery<PersonDocument>
    {
        protected override PersonDocument ExecuteQuery(IOrderedQueryable<PersonDocument> query)
        {
            return query.Where(x => x.FirstName == "Tom").AsEnumerable().FirstOrDefault();
        }
    }
}
