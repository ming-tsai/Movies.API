using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Core.Interfaces;
using Movies.Core.Requests;
using Movies.Core.Responses;
using Movies.Domain.Entities;
using Movies.Domain.Migrations;
using Movies.Core.Utils;

namespace Movies.BL.Interfaces;
public class MovieService : BaseService, IMovieService
{
    public MovieService(AppDbContext dbContext, ILogger<MovieService>? logger)
        : base(dbContext, logger)
    {
    }

    public async Task<Response<MovieResponse>?> GetAsync(MovieGetRequest request)
    {
        Check.NotNull(request, nameof(request));
        var query = GetQuery(request);
        Response<MovieResponse>? result = null;
        if (await query.AnyAsync().ConfigureAwait(false))
        {
            var queryPaging = await query
                                        .Select(x => MapToResponse(x))
                                        .Skip(request.SkipSize)
                                        .Take(request.PageSize)
                                        .ToListAsync()
                                        .ConfigureAwait(false);
            var total = await query.CountAsync().ConfigureAwait(false);
            result = new Response<MovieResponse>()
            {
                Items = queryPaging,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Total = total
            };
        }
        return result;
    }

    private IQueryable<Movie> GetQuery(MovieGetRequest request)
    {
        var query = DbContext.Movies;
        var nameFilter = query!.Where(nameof(Movie.Name), request.Name);
        var descriptionFilter = nameFilter!.Where(nameof(Movie.Description), request.Description);
        var typeFilter = descriptionFilter!.Where(nameof(Movie.Type), request.Type);
        return ApplyReleaseDateFilter(typeFilter, request.ReleaseDate);
    }

    private static IQueryable<Movie> ApplyReleaseDateFilter(IQueryable<Movie> query, DateTime? date)
    {
        return date == null ?
                query :
                query.Where(x =>
                        x.ReleaseDate != null &&
                        x.ReleaseDate.Value.Date == x.ReleaseDate.Value.Date);
    }

    public async Task<MovieResponse?> CreateAsync(MoviePostRequest request)
    {
        Check.NotNull(request, nameof(request));
        MovieResponse? result = null;
        var isExists = await DbContext
                                .Movies!
                                .AnyAsync(m => m.Name == request.Name)
                                .ConfigureAwait(false);
        if (!isExists)
        {
            var movie = new Movie()
            {
                Description = request.Description!,
                IsEnabled = true,
                Name = request.Name!,
                ReleaseDate = request.ReleaseDate,
                Type = request.Type!,
                User = request.User,
            };
            await DbContext.AddAsync(movie).ConfigureAwait(false);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            result = MapToResponse(movie);
        }
        return result;
    }

    public Task<MovieResponse?> UpdateAsync(int movieId, MoviePatchRequest request)
    {
        return UpdateDataAsync(movieId, request);
    }

    public Task<MovieResponse?> DisableAsync(int movieId, AuditableRequest request)
    {
        Check.NotNull(request, nameof(request));
        return UpdateDataAsync(movieId, new MoviePatchRequest()
        {
            IsEnabled = false,
            User = request.User
        });
    }

    private async Task<MovieResponse?> UpdateDataAsync(int movieId, MoviePatchRequest request)
    {
        Check.NotNull(request, nameof(request));
        var data = await DbContext
                            .Movies!
                            .FirstOrDefaultAsync(m => m.Id == movieId)
                            .ConfigureAwait(false);
        MovieResponse? result = null;
        if (data != null)
        {
            data.Name = request.Name ?? data.Name!;
            data.Description = request.Description ?? data.Description!;
            data.Type = request.Type ?? data.Type!;
            data.ReleaseDate = request.ReleaseDate ?? data.ReleaseDate;
            data.IsEnabled = request.IsEnabled ?? data.IsEnabled;
            data.User = request.User;
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            result = MapToResponse(data);
        }
        return result;
    }

    public MovieResponse MapToResponse(Movie movie)
    {
        Check.NotNull(movie, nameof(movie));
        return new MovieResponse()
        {
            Id = movie.Id,
            Name = movie.Name,
            AverageRating = movie.AverageRating,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            UpdatedAt = movie.UpdatedAt,
            User = movie.User
        };
    }

    public async Task<Response<MovieReviewResponse>?> GetReviewsAsync(int movieId, GetRequest request)
    {
        Check.NotNull(request, nameof(request));
        var query = DbContext.MovieReviews!.Where(mr => mr.MovieId == movieId);
        Response<MovieReviewResponse>? result = null;
        if (await query.AnyAsync().ConfigureAwait(false))
        {
            var queryPaging = await query
                                        .Select(x => MapToResponse(x))
                                        .Skip(request.SkipSize)
                                        .Take(request.PageSize)
                                        .ToListAsync()
                                        .ConfigureAwait(false);
            var total = await query.CountAsync().ConfigureAwait(false);
            result = new Response<MovieReviewResponse>()
            {
                Items = queryPaging,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Total = total
            };
        }
        return result;
    }

    public MovieReviewResponse MapToResponse(MovieReview review)
    {
        Check.NotNull(review, nameof(review));
        return new MovieReviewResponse()
        {
            Rating = review.Rating,
            Comment = review.Comment,
            UpdatedAt = review.UpdatedAt,
            User = review.User
        };
    }
}