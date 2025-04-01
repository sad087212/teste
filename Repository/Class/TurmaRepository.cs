using FIPECAFI.Data;
using FIPECAFI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Repository.Class;

public class TurmaRepository : ITurmaRepository
{
        private readonly ApplicationDbContext _context;

        public TurmaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Turma>> GetAllAsync(string? descricao,string? email, int? cursoId)
        {
            var query = _context.Turmas.Include(t => t.Curso).AsQueryable();

            if (!string.IsNullOrEmpty(descricao))
            {
                descricao = descricao.ToLower();
                query = query.Where(t => t.Descricao.ToLower().Contains(descricao));
            }

            if (cursoId.HasValue)
            {
                query = query.Where(t => t.CursoId == cursoId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Turma?> GetByIdAsync(int id)
        {
            return await _context.Turmas.Include(t => t.Curso).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateAsync(Turma turma)
        {
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Turma turma)
        {
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma != null)
            {
                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();
            }
        }
}