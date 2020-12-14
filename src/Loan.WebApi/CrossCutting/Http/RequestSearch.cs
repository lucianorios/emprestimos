using Loan.WebApi.Data.Abstraction;

namespace Loan.WebApi.CrossCutting.Http
{
    public class RequestSearch<T> : IRequestSearch<T>
    {
        public T Search { get; set; }
        public int Page { get; set; }
        public int? PageSize { get; set; }

        public RequestSearch() { }

        public RequestSearch(int page, int? pageSize, T search) : this()
        {
            Search = search;
            Page = page;
            PageSize = pageSize;
        }
    }
}
