using Movies.Core.Requests;
using Movies.Core.Responses;
using Movies.Domain.Entities;

namespace Movies.Core.Interfaces;
public interface IMovieService
{
    Task<Response<MovieResponse>?> GetAsync(MovieGetRequest request);
    Task<Response<MovieReviewResponse>?> GetReviewsAsync(int movieId, GetRequest request);
    Task<MovieResponse?> CreateAsync(MoviePostRequest request);
    Task<MovieResponse?> UpdateAsync(int movieId, MoviePatchRequest request);
    Task<MovieResponse?> DisableAsync(int movieId, AuditableRequest request);
}