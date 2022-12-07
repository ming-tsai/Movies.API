using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using System.Diagnostics.CodeAnalysis;


namespace Movies.Domain.Migrations;
public class AppDbContext : DbContext
{
    public DbSet<Movie>? Movies { get; set; }
    public DbSet<MovieReview>? MovieReviews { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder != null)
        {
            modelBuilder.Entity<Movie>()
                .HasData(new List<Movie>()
                {
                    new Movie()
                    {
                        Id = 1,
                        Name = "The Avengers",
                        Description = "Nick Fury is compelled to launch the Avengers Initiative when" +
                            "Loki poses a threat to planet Earth. His squad of superheroes put their " +
                            "minds together to accomplish the task.",
                        Type = "Adventure/Action",
                        IsEnabled = false,
                        UpdatedAt = new DateTime(2022, 12, 5),
                        User = "mtsai",
                        ReleaseDate = new DateTime(2012, 4, 26)
                    },
                    new Movie()
                    {
                        Id = 2,
                        Name = "Black Panther: Wakanda Forever",
                        Description = "Queen Ramonda, Shuri, M'Baku, Okoye and the Dora Milaje " +
                            "fight to protect their nation from intervening world powers in the " +
                            "wake of King T'Challa's death. As the Wakandans strive to embrace their " +
                            "next chapter, the heroes must band together with Nakia and Everett Ross to " +
                            "forge a new path for their beloved kingdom",
                        Type = "Adventure/Action",
                        IsEnabled = true,
                        UpdatedAt = new DateTime(2022, 12, 5),
                        User = "mtsai",
                        ReleaseDate = new DateTime(2022, 11, 11)
                    }
                });
            base.OnModelCreating(modelBuilder);
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (ChangeTracker.HasChanges())
        {
            var updatedAt = DateTime.Now;
            var auditColumnName = nameof(Auditable.UpdatedAt);
            var modified = ChangeTracker.Entries()
                            .Where(x =>
                                x.State == EntityState.Added ||
                                x.State == EntityState.Modified ||
                                x.State == EntityState.Deleted)
                            .Where(x => x.Entity is Auditable);
            foreach (var item in modified)
            {
                item.CurrentValues[auditColumnName] = updatedAt;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}