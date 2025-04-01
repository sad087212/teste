﻿namespace FIPECAFI.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(string? name, string? email, int? course);
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}