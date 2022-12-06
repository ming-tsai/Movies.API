namespace Movies.Domain.Entities;
public abstract class Auditable
{
    public DateTime UpdatedAt { get; set; }
    public string? User { get; set; }
}