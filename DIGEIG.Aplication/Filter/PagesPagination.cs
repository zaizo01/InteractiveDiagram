using System.Collections.Generic;

namespace DIGEIG.Application.Filter
{
    public class PagesPagination<T>
    {
        public int TotalRecords { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
