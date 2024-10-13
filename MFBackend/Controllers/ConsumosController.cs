using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MFBackend.Data;
using Microsoft.EntityFrameworkCore;
using MFBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace MFBackend.Controllers
{
 [Authorize]
    public class ConsumosController : Controller
    {
        private readonly AppDbContext _context;

        public ConsumosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Consumos.Include(c => c.Veiculo);
            return View(await appDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Descricao, Data, Valor, Km, Tipo, VeiculoId")] Consumo consumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Registra os erros de validação no console
            }
        }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", consumo.VeiculoId);
            return View(consumo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consumos == null)
                return NotFound();
            
            var consumo = await _context.Consumos.FindAsync(id);

            if(consumo == null)
                return NotFound();

            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", consumo.VeiculoId);
            return View(consumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Descricao, Data, Valor, Km, Tipo, VeiculoId")] Consumo consumo)
        {
            if(id != consumo.Id)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumoExists(consumo.Id))
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
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", consumo.VeiculoId);
            return View(consumo);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.Consumos == null) 
                return NotFound();
            
            var consumo = await _context.Consumos.Include(c => c.Veiculo).FirstOrDefaultAsync(m => m.Id == id);

            if(consumo == null)
                return NotFound();

            return View(consumo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Consumos == null)
                return NotFound();
            
            var consumo = await _context.Consumos.Include(c => c.Veiculo).FirstOrDefaultAsync(m => m.Id == id);

            if(consumo == null)
                return NotFound();

            return View(consumo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if(_context.Consumos == null)
                return Problem("Erro ao deletar o registro, tente novamente mais tarde.");

            var consumo = await _context.Consumos.FindAsync(id);   

            if(consumo != null)
            {
                _context.Consumos.Remove(consumo);
            }
        
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool ConsumoExists(int id)
        {
            return _context.Consumos.Any(e => e.Id == id);
        }
    }
}