using Microsoft.EntityFrameworkCore;
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
    }
}