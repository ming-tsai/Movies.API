using Movies.Domain.Entities;

namespace Movies.Core.Responses;
public class MovieResponse : Auditable
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal AverageRating { get; set; }
    public DateTime? ReleaseDate { get; set; }
}