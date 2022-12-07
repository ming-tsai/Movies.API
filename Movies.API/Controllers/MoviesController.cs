using Microsoft.AspNetCore.Mvc;
using Movies.Core.Interfaces;
using Movies.Core.Requests;
using Movies.Core.Responses;

namespace Movies.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly IMovieService _service;

    public MoviesController(IMovieService service, ILogger<MoviesController> logger)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<MovieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<Response<MovieResponse>?> GetAsync([FromQuery] MovieGetRequest request)
        => _service.GetAsync(request);

    [HttpGet("{movieId}/Reviews")]
    [ProducesResponseType(typeof(Response<MovieReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<Response<MovieReviewResponse>?> GetReviewsAsync(
        [FromRoute] int movieId, [FromQuery] GetRequest request
    ) => _service.GetReviewsAsync(movieId, request);
}
