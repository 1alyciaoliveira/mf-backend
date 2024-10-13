using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MFBackend.Models;
using MFBackend.Data;
using Microsoft.EntityFrameworkCore;

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

}