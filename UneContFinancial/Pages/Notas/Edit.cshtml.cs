using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;
using UneContFinancial.Models;
using UneContFinancial.Services;

namespace UneContFinancial.Pages.Notas
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Nota Nota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var nota =  await _context.Notas.FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null) return NotFound();

            Nota = nota;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile NotaFiscal, IFormFile BoletoBancario)
        {
            try
            {
                if (NotaService.ValidaSeNotaFiscalFoiSelecionado(NotaFiscal)) Nota.NotaFiscal = NotaService.SalvarNotaFiscal(NotaFiscal);
                if (NotaService.ValidaSeBoletolFoiSelecionado(BoletoBancario)) Nota.BoletoBancario = NotaService.SalvarBoletoBancario(BoletoBancario);

                _context.Attach(Nota).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(Nota.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
