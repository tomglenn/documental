using System.Linq;
using Microsoft.Azure.Documents;

namespace Documental.Queries
{
    public abstract class MultipleDocumentQuery<T> where T : Document
    {
        protected abstract IQueryable<T> ExecuteQuery(IOrderedQueryable<T> query);

        public IQueryable<T> Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query);
        }
    }

    public abstract class MultipleDocumentQuery<T, TReturn> where T : Document
    {
        protected abstract IQueryable<TReturn> ExecuteQuery(IOrderedQueryable<T> query);

        public IQueryable<TReturn> Execute(IOrderedQueryable<T> query)
        {
            return ExecuteQuery(query);
        }
    }
}
