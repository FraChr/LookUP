namespace API.Model;

public class PageResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int Total { get; set; }
}