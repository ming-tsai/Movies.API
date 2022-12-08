using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Requests;
public class MovieReviewPostRequest : AuditableRequest
{
    [Required]
    [StringLength(10000, MinimumLength = 10)]
    public string? Comment { get; set; }
    [Required]
    [Range(0.01, 5)]
    public decimal Rating { get; set; }
}