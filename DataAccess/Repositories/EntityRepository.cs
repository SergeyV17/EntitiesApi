using Application.DbContexts;
using Application.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class EntityRepository : IEntityRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public EntityRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<Entity> InsertEntityAsync(Entity entity)
    {
        await using var appDbContext = await _dbContextFactory.CreateDbContextAsync();
        var insertedEntity = await appDbContext.AddAsync(entity);
        await appDbContext.SaveChangesAsync();

        return insertedEntity.Entity;
    }

    public async Task<Entity?> GetEntityByIdAsync(Guid id)
    {
        await using var appDbContext = await _dbContextFactory.CreateDbContextAsync();
        return await appDbContext.Entities.FirstOrDefaultAsync(e => e.Id == id);
    }
}