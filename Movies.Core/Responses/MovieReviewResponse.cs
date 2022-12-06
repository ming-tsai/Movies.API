using Movies.Domain.Entities;

namespace Movies.Core.Responses;
public class MovieReviewResponse : Auditable
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
}