using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Requests;
public class MovieDeleteRequest : AuditableRequest
{
    [Range(int.MaxValue, 1)]
    public int Id { get; set; }
}