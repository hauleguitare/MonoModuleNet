﻿using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Common.Attribute;

namespace Infrastructure.DAL.Repository;

public interface IEntityRepository<TEntity, in TKey>: IRepository where TEntity : IAggregateRoot
{
    public IQueryable<TEntity> AsQueryable();
    public TEntity? GetById(TKey id);
    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token);
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void AddRange(ICollection<TEntity> entities);
    Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
    void UpdateRange(ICollection<TEntity> entities);
    void RemoveRange(ICollection<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Remove(TKey id);
}

[Injectable(InterfaceType = typeof(IEntityRepository<,>), Lifetime = ServiceLifetime.Scoped)]
public class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey> where TEntity : class, IAggregateRoot 
{
    private readonly DbSet<TEntity> _entity;

    // public EntityRepository(ApplicationDbContext context)
    // {
    //     _entity = context.Instance.Set<TEntity>();
    // }
    
    public EntityRepository()
    {
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _entity.AsQueryable();
    }

    public TEntity? GetById(TKey id)
    {
        return _entity.Find(id);
    }

    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token)
    {
        return _entity.FindAsync(id).AsTask();
    }

    public void Add(TEntity entity)
    {
        _entity.Add(entity);
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return _entity.AddAsync(entity, cancellationToken).AsTask();
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        _entity.AddRange(entities);
    }

    public Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return _entity.AddRangeAsync(entities, cancellationToken);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        _entity.UpdateRange(entities);
    }

    public void RemoveRange(ICollection<TEntity> entities)
    {
        _entity.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _entity.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public void Remove(TKey id)
    {
        var entity = _entity.Find(id);

        if (entity is not null)
        {
            _entity.Remove(entity);
        }
    }

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                //TODO:
                // Implement context dispose here
            }
        }
        this._disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}