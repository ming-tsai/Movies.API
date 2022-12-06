namespace Movies.Core.Responses;
public class Response<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPage => PageSize == 0 ? 1 : (int)Math.Ceiling((decimal)Total / PageSize);
    public int Total { get; set; }
}