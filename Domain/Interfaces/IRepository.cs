﻿namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Remove(Guid id);
    Task Update(T entity);
}