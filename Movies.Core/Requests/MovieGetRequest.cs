namespace Movies.Core.Requests;
public class MovieGetRequest : GetRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Type { get; set; }
}