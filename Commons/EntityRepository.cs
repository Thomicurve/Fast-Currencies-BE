﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace fast_currencies_be;

public class EntityRepository<T> where T : EntityBase
{
    private readonly FastCurrenciesContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityRepository(FastCurrenciesContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public T? GetById(int id)
    {
        return _dbSet.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query;
    }

    public T Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public bool Update(T entity)
    {
        T? entityExisting = _dbSet.FirstOrDefault(x => x.Id == entity.Id);
        if (entityExisting == null) {
            throw new Exception("No se encontró el registro");
        }
        
        _context.Update(entity);
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        T? entity = _dbSet.FirstOrDefault(x => x.Id == id);

        if(entity == null) {
            throw new Exception("No se encontró el registro");
        }

        _dbSet.Remove(entity);
        _context.SaveChanges();
        return true;
    }
}
