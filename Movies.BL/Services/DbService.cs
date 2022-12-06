using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Movies.Core.Interfaces;
using Movies.Core.Requests;
using Movies.Core.Responses;
using Movies.Domain.Entities;
using Movies.Domain.Migrations;

namespace Movies.BL.Interfaces;
public class BaseService
{
    protected AppDbContext DbContext { get; private set; }
    public BaseService(
        [NotNull]AppDbContext dbContext,
        [AllowNull]ILogger<BaseService>? logger)
    {
        DbContext = dbContext;
    }
}