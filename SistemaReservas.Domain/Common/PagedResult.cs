namespace SistemaReservas.Domain.Common
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> items)
        {
            Items = items;
        }
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
