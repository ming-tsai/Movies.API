using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities;

[Table("MovieReviews")]
public class MovieReview : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public decimal Rating { get; set; }
    public string? Comment { get; set; }
    [ForeignKey(nameof(MovieId))]
    public virtual Movie? Movie { get; set; }
}