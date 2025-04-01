using System.Diagnostics;
using FIPECAFI.Data;
using FIPECAFI.Models;
using FIPECAFI.Repository.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Controllers;

public class CursoController : Controller
{
    readonly ApplicationDbContext _context;

    private readonly ICourseRepository _repository;

    public CursoController(ICourseRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }
    
    public async Task<IActionResult> Index(string descricao, string nome, int cursoId)
    {
        var curso = await _repository.GetAllAsync(descricao, nome, cursoId);
        return View(curso);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Store(Curso curso)
    {
        if (!ModelState.IsValid)
        {
            return View("Create");
        }

        try
        {
            _repository.CreateAsync(curso);
            TempData["SuccessMessage"] = "Curso cadastrado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["ErrorMessage"] = e.Message;
            throw;
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var curso = await _repository.GetByIdAsync(id);
        if (curso == null)
        {
            return NotFound();
        }
        
        return View(curso);
    }

    public async Task<IActionResult> Update(Curso curso)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", curso);
        }

        try
        {
            await _repository.UpdateAsync(curso);
            TempData["SuccessMessage"] = "Curso atualizado com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost]
    [Route("Curso/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var curso = await _repository.GetByIdAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        var hasTurmas = await _context.Turmas.AnyAsync(t => t.CursoId == id);
        if (hasTurmas)
        {
            TempData["ErrorMessage"] = "Erro: Não é possível excluir o curso, pois ele tem turmas cadastradas.";
            return RedirectToAction("Index");
        }

        await _repository.DeleteAsync(id);
        TempData["SuccessMessage"] = "Curso removido com sucesso!";
        return RedirectToAction("Index");
    }

}