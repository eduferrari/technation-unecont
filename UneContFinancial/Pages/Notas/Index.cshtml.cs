using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;
using UneContFinancial.Models;
using UneContFinancial.Services;

namespace UneContFinancial.Pages.Notas
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public Paginacao<Nota> Nota { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10;
            IQueryable<Nota> notas = from n in _context.Notas
                                       orderby n.DataEmissaoNota descending
                                       select n;

            Nota = await Paginacao<Nota>.CreateAsync(notas.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
