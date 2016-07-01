using System.Linq;
using Documental.Queries;
using Documental.Samples.Console.Documents;

namespace Documental.Samples.Console.Queries
{
    public class FindPeopleBySurnameQuery : MultipleDocumentQuery<PersonDocument>
    {
        private readonly string surname;

        public FindPeopleBySurnameQuery(string surname)
        {
            this.surname = surname;
        }

        protected override IQueryable<PersonDocument> ExecuteQuery(IOrderedQueryable<PersonDocument> query)
        {
            return query.Where(x => x.LastName == surname);
        }
    }
}
