using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Movies.Domain.Entities;

[Table("Movies")]
public class Movie : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, DisallowNull]
    public string? Name { get; set; }
    [Required, DisallowNull]
    public string? Description { get; set; }
    [Required, DisallowNull]
    public string? Type { get; set; }
    public DateTime? ReleaseDate { get; set; }
    [DefaultValue(true)]
    public bool IsEnabled { get; set; }
    public decimal AverageRating => Reviews != null && Reviews.Any() ?
                        Reviews.Sum(r => r.Rating) / Reviews.Count() : 0.00m;
    public virtual ICollection<MovieReview>? Reviews { get; set; }
}