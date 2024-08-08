using FitPathPro.Domain.Common.BaseEntity;

namespace FitPathPro.Application.Common.Interfaces;

/// <summary>
/// Base repository interface
/// This include basic CRUD methods
/// </summary>
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task AddAsync(T entity);

    void Update(T entity);

    Task Delete(int id);
}

