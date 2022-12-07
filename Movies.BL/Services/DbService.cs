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
    protected readonly AppDbContext DbContext;
    protected readonly ILogger<BaseService>? _logger;
    public BaseService(
        [NotNull] AppDbContext dbContext,
        [AllowNull] ILogger<BaseService>? logger)
    {
        DbContext = dbContext;
        _logger = logger;
    }
}