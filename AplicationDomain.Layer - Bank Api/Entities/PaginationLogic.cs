using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public class PaginationLogic<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPages => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPages ? CurrentPage - 1 : (int?)null;

        public PaginationLogic(List<T> items, int count, int pageNumber, int pageSize) 
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PaginationLogic<T> Pagination(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            var count = items.Count();
            var paginationLogic = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationLogic<T>(paginationLogic, count, pageNumber, pageSize);
        }
    }
}
