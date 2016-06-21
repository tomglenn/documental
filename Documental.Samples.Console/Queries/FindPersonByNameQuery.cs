using System.Linq;
using Documental.Queries;
using Documental.Samples.Console.Documents;

namespace Documental.Samples.Console.Queries
{
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
}
