namespace Con_Mgmt_Cach_Pag_RateLimiting.Models
{
    public class PagedResponse<T>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }

        public PagedResponse(List<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
