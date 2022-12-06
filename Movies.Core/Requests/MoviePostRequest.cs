using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Requests;
public class MoviePostRequest : AuditableRequest
{
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Name { get; set; }
    [Required]
    [StringLength(10000, MinimumLength = 10)]
    public string? Description { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Type { get; set; }
    public DateTime? ReleaseDate { get; set; }
}