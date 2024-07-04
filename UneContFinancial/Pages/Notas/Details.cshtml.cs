using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;
using UneContFinancial.Models;

namespace UneContFinancial.Pages.Notas
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Nota Nota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var nota = await _context.Notas.FirstOrDefaultAsync(m => m.Id == id);
            
            if (nota == null) return NotFound();

            Nota = nota;

            return Page();
        }
    }
}
