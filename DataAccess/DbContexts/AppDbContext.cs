using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : 
        base(options)
    {

    }

    public DbSet<Entity> Entities => Set<Entity>();
}