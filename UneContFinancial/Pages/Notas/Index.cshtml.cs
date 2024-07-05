using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        [BindProperty(SupportsGet = true)]
        public int? TipoFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroMes { get; set; }

        [BindProperty(SupportsGet = true)]
        public StatusNota FiltroStatus { get; set; }

        public Paginacao<Nota> Nota { get;set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 10;
            var filtroMes = !FiltroMes.IsNullOrEmpty() ? DateTime.Parse(FiltroMes) : DateTime.Now;

            IQueryable<Nota> notas = _context.Notas.AsNoTracking().OrderByDescending(x => x.DataEmissaoNota);

            switch (TipoFiltro)
            {
                case 1:
                    notas = notas.Where(x => x.DataEmissaoNota.Value.Year == filtroMes.Year && x.DataEmissaoNota.Value.Month == filtroMes.Month);
                    break;
                case 2:
                    notas = notas.Where(x => x.Status == StatusNota.CobrancaRealizada && x.DataCobranca.HasValue && x.DataCobranca.Value.Year == filtroMes.Year && x.DataCobranca.Value.Month == filtroMes.Month);
                    break;
                case 3:
                    notas = notas.Where(x => x.Status == StatusNota.PagamentoRealizado && x.DataPagamento.HasValue && x.DataPagamento.Value.Year == filtroMes.Year && x.DataPagamento.Value.Month == filtroMes.Month);
                    break;
                case 4:
                    notas = notas.Where(x => x.Status == FiltroStatus);
                    break;
            }

            Nota = await Paginacao<Nota>.CreateAsync(notas, pageIndex ?? 1, pageSize);
        }
    }
}
