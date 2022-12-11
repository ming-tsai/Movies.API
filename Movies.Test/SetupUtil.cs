using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movies.API.Controllers;
using Movies.BL.Interfaces;
using Movies.Core.Interfaces;
using Movies.Domain.Entities;
using Movies.Domain.Migrations;
using NUnit.Framework;

namespace Movies.Test;

[TestFixture]
public class SetupUtil
{
    protected virtual DbContextOptions<AppDbContext>? Options { get; private set; }
    protected virtual IServiceProvider? ServiceProvider { get; private set; }
    [SetUp]
    public async Task Main()
    {
        Options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;
        using var context = new AppDbContext(Options);
        await context.Movies!.AddRangeAsync(new List<Movie>()
        {
            new Movie()
            {
                Id = 3,
                IsEnabled = false,
                Description = "some test",
                Name = "some name",
                Type = "some type 1",
                User = "testuser2",
                UpdatedAt = DateTime.Now,
                ReleaseDate = DateTime.Today,
                Reviews = new List<MovieReview>()
                {
                    new MovieReview()
                    {
                        Id = 1,
                        MovieId = 3,
                        UpdatedAt = DateTime.Now,
                        User = "testuser",
                        Rating = 1,
                        Comment = "some comment"
                    }
                }
            },
            new Movie()
            {
                Id = 4,
                IsEnabled = true,
                Type = "some type 2",
                User = "testuser",
                UpdatedAt = DateTime.Now,
                Description = "movie 2 description",
                Name = "movie 2",
                ReleaseDate = DateTime.Today.AddYears(-1)
            }
        });

        await context.Database.EnsureCreatedAsync();
        await context.SaveChangesAsync();

        ServiceProvider = new ServiceCollection()
                                .AddLogging()
                                .BuildServiceProvider();
        var services = new ServiceCollection();
        services.AddSingleton(Options);
        services.AddScoped<AppDbContext>();
        services.AddTransient<IMovieService, MovieService>();
        services.AddSingleton<MoviesController>();

        var factory = ServiceProvider.GetService<ILoggerFactory>();
        services.AddSingleton(factory!.CreateLogger<MovieService>());
        ServiceProvider = services.BuildServiceProvider();
    }
}