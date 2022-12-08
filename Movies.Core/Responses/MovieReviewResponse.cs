using Movies.Domain.Entities;

namespace Movies.Core.Responses;
public class MovieReviewResponse : Auditable
{
    public decimal Rating { get; set; }
    public string? Comment { get; set; }
}