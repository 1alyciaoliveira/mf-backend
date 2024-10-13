using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MFBackend.Data;
using Microsoft.EntityFrameworkCore;
using MFBackend.Models;

namespace MFBackend.Controllers;

public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Usuarios.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id, Nome, Senha, Perfil")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        return View(usuario);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Usuarios == null)
            return NotFound();
        
        var usuario = await _context.Usuarios.FindAsync(id);

        if(usuario == null)
            return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Nome, Senha, Perfil")] Usuario usuario)
    {
        if(id != usuario.Id)
            return NotFound();
        
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id == null || _context.Usuarios == null)
            return NotFound();
        
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

        if(usuario == null)
            return NotFound();

        return View(usuario);
    }

        public async Task<IActionResult> Delete(int? id)
    {
        if(id == null || _context.Usuarios == null)
            return NotFound();
        
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

        if(usuario == null)
            return NotFound();

        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if(_context.Usuarios == null)
            return Problem("Ocorreu um erro ao tentar excluir o registro!");
        
        var usuario = await _context.Usuarios.FindAsync(id);

        if(usuario != null)
            _context.Usuarios.Remove(usuario);
        
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }

    
}