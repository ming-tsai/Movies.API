using Movies.Core.Requests;
using Movies.Core.Responses;
using Movies.Domain.Entities;

namespace Movies.Core.Interfaces;
public interface IMovieService
{
    Task<Response<MovieResponse>?> GetAsync(MovieGetRequest request);
    Task<Response<MovieReviewResponse>?> GetReviewsAsync(int movieId, GetRequest request);
    Task<MovieResponse?> DisableAsync(MovieDeleteRequest request);
    Task<MovieResponse?> CreateAsync(MoviePostRequest request);
}