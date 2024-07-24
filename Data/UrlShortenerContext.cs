using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

public class UrlShortenerContext : DbContext
{
    public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options)
        : base(options)
    {

    }
    public DbSet<UrlMapping> UrlMappings { get; set; }
}

