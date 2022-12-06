using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Requests;
public class MoviePatchRequest : AuditableRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public bool? IsEnabled { get; set; }
}