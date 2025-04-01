using FIPECAFI.Data;
using FIPECAFI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Repository;

public class LeadsRepository : ILeadsRepository
{
    readonly ApplicationDbContext _context;
    
    public LeadsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lead>> GetAllLeadsAsync(string? name, string? email, string? course)
    {
        var query = _context.Leads.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            name = name.ToLower();
            query = query.Where(l => l.Nome.ToLower().Contains(name));
        }

        if (!string.IsNullOrEmpty(email))
        {
            email = email.ToLower();
            query = query.Where(l => l.Email.ToLower().Contains(email));
        }

        if (!string.IsNullOrEmpty(course))
        {
            course = course.ToLower();
            query = query.Where(l => l.CursoInteresse.ToLower().Contains(course));
        }

        return await query.ToListAsync();
    }



    public Lead Create(Lead lead)
    {
        _context.Leads.Add(lead);
        _context.SaveChanges();
        return lead;
    }

    public async Task<Lead?> GetByIdAsync(int id)
    {
        return await _context.Leads.FindAsync(id);
    }

    public async Task UpdateAsync(Lead lead)
    {
        _context.Leads.Update(lead);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead != null)
        {
            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
        }
    }
  
}