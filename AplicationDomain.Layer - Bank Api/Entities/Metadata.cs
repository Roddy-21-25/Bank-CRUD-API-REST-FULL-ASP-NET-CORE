using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomain.Layer___Bank_Api.Entities
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPages { get; set; }

        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}
