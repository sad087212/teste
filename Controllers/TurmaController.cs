using FIPECAFI.Data;
using FIPECAFI.Models;
using FIPECAFI.Repository.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIPECAFI.Controllers;

public class TurmaController : Controller
{
    private readonly ITurmaRepository _repository;
    private readonly ApplicationDbContext _context;

    public TurmaController(ITurmaRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index(string descricao, string email,int? cursoId)
    {
        var turmas = await _repository.GetAllAsync(descricao, email,cursoId);
        ViewBag.Cursos = _context.Cursos.ToList();
        return View(turmas);
    }

    public IActionResult Create()
    {
        ViewBag.Cursos = _context.Cursos.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Store(Turma turma)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cursos = _context.Cursos.ToList();
            return View("Create");
        }

        try
        {
            await _repository.CreateAsync(turma);
            TempData["SuccessMessage"] = "Turma cadastrada com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            Console.WriteLine(e);
            return View("Create");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var turma = await _repository.GetByIdAsync(id);

        if (turma == null)
        {
            return NotFound();
        }

        ViewBag.Cursos = _context.Cursos.ToList();
        return View(turma);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Turma turma)
    {
        ModelState.Remove("Curso");
        if (!ModelState.IsValid)
        {
            ViewBag.Cursos = _context.Cursos.ToList();
            return View("Edit", turma);
        }

        try
        {
            await _repository.UpdateAsync(turma);
            TempData["SuccessMessage"] = "Turma atualizada com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            Console.WriteLine(e);
            return View("Edit");
        }
    }

    [HttpPost]
    [Route("Turma/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var turma = await _repository.GetByIdAsync(id);

        if (turma == null)
        {
            return NotFound();
        }

        try
        {
            await _repository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Turma excluída com sucesso!";
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            Console.WriteLine(e);
        }

        return RedirectToAction("Index");
    }
    

}
