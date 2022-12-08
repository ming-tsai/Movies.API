using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Requests;
public class AuditableRequest
{
    [Required]
    [StringLength(300, MinimumLength = 5)]
    public string? User { get; set; }
}