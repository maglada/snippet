using Microsoft.EntityFrameworkCore;
using snippet.api.Models;

namespace snippet.api.Data;

/// <summary>
/// Database context class for managing code snippets in the application.
/// This class serves as the main entry point for database interactions using Entity Framework Core.
/// </summary>
/// <remarks>
/// Responsibilities:
/// - Defines the database structure and relationships
/// - Manages database connections and transactions
/// - Provides DbSet properties for accessing snippet data
/// - Configures entity relationships and constraints
/// </remarks>
public class SnippetContext : DbContext
{
    public SnippetContext(DbContextOptions<SnippetContext> options) 
        : base(options)
    {
    }

    public DbSet<Snippet> Snippets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Snippet entity
        modelBuilder.Entity<Snippet>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Code)
                .IsRequired();
            
            entity.Property(e => e.Title)
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            // Index for faster language queries
            entity.HasIndex(e => e.Language);
        });
    }
}