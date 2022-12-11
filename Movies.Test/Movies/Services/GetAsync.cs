using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Interfaces;

namespace Movies.Test.Movies.Services;

[TestFixture]
internal class GetAsync : SetupUtil
{
    [TestCase("some", 1)]
    [TestCase("movie", 1)]
    [TestCase(null, 4)]
    [TestCase("fsdfsaf", null)]
    public async Task PassGetNameFilter_ShouldGetExpectedCount(string? value, int? expected)
    {
        var response = await ServiceProvider!.GetService<IMovieService>()!
                            .GetAsync(new Core.Requests.MovieGetRequest()
                            {
                                Name = value
                            })!;
        Assert.That(response?.Total, Is.EqualTo(expected));
    }

    [TestCase("name", 2)]
    [TestCase("name desc", 1)]
    [TestCase("TyPe_desc", 4)]
    public async Task PassSortFilter_ShouldGetExpectedMovieId(string? sort, int? expected)
    {
        var response = await ServiceProvider!.GetService<IMovieService>()!
                            .GetAsync(new Core.Requests.MovieGetRequest()
                            {
                                Sort = sort
                            })!;
        Assert.That(response?.Items!.First().Id, Is.EqualTo(expected));
    }
}