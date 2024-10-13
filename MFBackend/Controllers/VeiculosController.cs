using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MFBackend.Data;
using Microsoft.EntityFrameworkCore;
using MFBackend.Models;

namespace MFBackend.Controllers;

public class VeiculosController : Controller
{
    private readonly AppDbContext _context;

    public VeiculosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dados = await _context.Veiculos.ToListAsync();
        return View(dados);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Veiculos veiculo)
    {
        if (ModelState.IsValid)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }
        return View(veiculo);
    }
}