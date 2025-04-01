using FIPECAFI.Models;

namespace FIPECAFI.Repository;

public interface ILeadsRepository
{
    Task<List<Lead>> GetAllLeadsAsync(string? name, string? email, string? course);
    Lead Create(Lead lead);
    Task<Lead?> GetByIdAsync(int id);
    Task UpdateAsync(Lead lead);
    Task DeleteAsync(int id);
}
