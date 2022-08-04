
namespace DIGEIG.Application.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Search
        {
            get
            {
                if (string.IsNullOrEmpty(LocalSearch))
                {
                    return string.Empty;
                }
                else
                    return LocalSearch;
            }

            set { LocalSearch = value; }
        }

        public string ColumnOrdeBy { get; set; }

        public bool IsOrderByDescending { get; set; }

        private string LocalSearch { get; set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            Search = string.Empty;
            IsOrderByDescending = true;
            ColumnOrdeBy = string.Empty;
        }

        public PaginationFilter(int pageNumber, int pageSize, string search, string columnOrdeBy, bool isOrderByDescending = true)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;

            if (string.IsNullOrEmpty(search))
                Search = string.Empty;
            else
                Search = search;

            if (string.IsNullOrEmpty(columnOrdeBy))
                ColumnOrdeBy = string.Empty;
            else
                ColumnOrdeBy = columnOrdeBy;

            IsOrderByDescending = isOrderByDescending;
        }

    }

}
