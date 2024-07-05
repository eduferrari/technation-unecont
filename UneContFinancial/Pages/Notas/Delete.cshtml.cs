using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;
using UneContFinancial.Models;

namespace UneContFinancial.Pages.Notas
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Nota Nota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas.FirstOrDefaultAsync(m => m.Id == id);

            if (nota == null) return NotFound();

            Nota = nota;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                Nota = nota;
                _context.Notas.Remove(Nota);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
