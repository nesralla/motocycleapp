namespace Motocycle.Application.Commons.Responses
{
    public class MetaDataResponse
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecords { get; private set; }
        public bool HasPrevious { get; private set; }
        public bool HasNext { get; private set; }

        public MetaDataResponse(int currentPage, int totalPages, int pageSize, int totalRecords, bool hasPrevious, bool hasNext)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            HasPrevious = hasPrevious;
            HasNext = hasNext;
        }
    }
}
