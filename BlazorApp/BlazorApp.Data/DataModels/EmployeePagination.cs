namespace BlazorApp.Helper
{
    public class EmployeePagination<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
