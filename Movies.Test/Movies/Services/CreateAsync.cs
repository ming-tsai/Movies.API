using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Interfaces;

namespace Movies.Test.Movies.Services;

[TestFixture]
internal class CreateAsync : SetupUtil
{
    [Test]
    public async Task PassCreateRequest_ShouldCreateSuccessfully()
    {
        var response = await ServiceProvider!.GetService<IMovieService>()!
                            .CreateAsync(new Core.Requests.MoviePostRequest()
                            {
                                Description = "someddd",
                                Name = "Some Movie 3",
                                ReleaseDate = DateTime.Today,
                                User = "ttt",
                                Type = "Some Type"
                            })!;
        Assert.That(response, Is.Not.Null);
    }
}