using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Core.Requests;
public class GetRequest
{
    [NotMapped]
    [Range(1, int.MaxValue, ErrorMessage = "Page index should be greater than 0")]
    public int PageIndex { get; set; } = 1;
    [NotMapped]
    [Range(1, int.MaxValue, ErrorMessage = "Page size should be greater than 0")]
    public int PageSize { get; set; } = 10;
    [NotMapped]
    public virtual int SkipSize => (PageIndex - 1) * PageSize;
    [NotMapped]
    public string? Sort { get; set; }
}