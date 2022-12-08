using Movies.Core.Requests;
using Movies.Core.Responses;
using Movies.Domain.Entities;

namespace Movies.Core.Interfaces;
public interface IMovieService
{
    Task<Response<MovieResponse>?> GetAsync(MovieGetRequest request);
    Task<MovieResponse?> CreateAsync(MoviePostRequest request);
    Task<MovieResponse?> UpdateAsync(int movieId, MoviePatchRequest request);
    Task<MovieResponse?> DisableAsync(int movieId, AuditableRequest request);
    Task<Response<MovieReviewResponse>?> GetReviewsAsync(int movieId, GetRequest request);
    Task<MovieReviewResponse?> CreateReviewAsync(int movieId, MovieReviewPostRequest request);
    Task<MovieReviewResponse?> UpdateReviewAsync(int reviewId, MovieReviewPatchRequest request);
}