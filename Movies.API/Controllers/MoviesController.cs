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
    [ActionName(nameof(GetAsync))]
    [ProducesResponseType(typeof(Response<MovieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<Response<MovieResponse>?> GetAsync([FromQuery] MovieGetRequest request)
        => _service.GetAsync(request);

    [HttpPost]
    [ProducesResponseType(typeof(Response<MovieResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] MoviePostRequest request)
    {
        IActionResult result = Accepted((string)"The movie is already added.");
        var response = await _service.CreateAsync(request).ConfigureAwait(false);
        if (response != null)
        {
            result = CreatedAtAction(nameof(GetAsync), response);
        }
        return result;
    }

    [HttpDelete("movieId")]
    [ProducesResponseType(typeof(Response<MovieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DisableAsync(
        [FromHeader] int movieId, [FromBody] AuditableRequest request)
    {
        IActionResult result = Accepted((string)"The movie does not exits.");
        var response = await _service.DisableAsync(movieId, request).ConfigureAwait(false);
        if (response != null)
        {
            result = Ok(response);
        }
        return result;
    }

    [HttpPatch("movieId")]
    [ProducesResponseType(typeof(Response<MovieResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(
        [FromHeader] int movieId, [FromBody] MoviePatchRequest request)
    {
        IActionResult result = Accepted((string)"The movie does not exits.");
        var response = await _service.UpdateAsync(movieId, request).ConfigureAwait(false);
        if (response != null)
        {
            result = Ok(response);
        }
        return result;
    }

    [HttpGet("{movieId}/Reviews"), ActionName(nameof(GetReviewsAsync))]
    [ProducesResponseType(typeof(Response<MovieReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<Response<MovieReviewResponse>?> GetReviewsAsync(
        [FromRoute] int movieId, [FromQuery] GetRequest request
    ) => _service.GetReviewsAsync(movieId, request);


    [HttpPost("{movieId}/Reviews")]
    [ProducesResponseType(typeof(Response<MovieReviewResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReviewAsync(
        [FromRoute] int movieId, [FromQuery] MovieReviewPostRequest request)
    {
        IActionResult result = Accepted((string)"The review is already added.");
        var response = await _service.CreateReviewAsync(movieId, request).ConfigureAwait(false);
        if (response != null)
        {
            result = CreatedAtAction(nameof(GetReviewsAsync), response);
        }
        return result;
    }

    [HttpPatch("Reviews/{reviewId}")]
    [ProducesResponseType(typeof(Response<MovieReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReviewAsync(
        [FromRoute] int reviewId, [FromQuery] MovieReviewPatchRequest request)
    {
        IActionResult result = Accepted((string)"The review does not exists.");
        var response = await _service.UpdateReviewAsync(reviewId, request).ConfigureAwait(false);
        if (response != null)
        {
            result = Ok(response);
        }
        return result;
    }
}
