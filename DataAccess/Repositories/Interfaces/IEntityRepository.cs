using Domain;

namespace Application.Repositories.Interfaces;

public interface IEntityRepository
{
    Task<Entity> InsertEntityAsync(Entity entity);
    Task<Entity?> GetEntityByIdAsync(Guid id);
}