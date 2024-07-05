using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;
using UneContFinancial.Models;

namespace UneContFinancial.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<int> InadimplenciaMensal { get; set; } = new List<int>();
    public List<string> MesesInadimplencia { get; set; } = new List<string>();

    public List<decimal> ReceitaMensal { get; set; } = new List<decimal>();
    public List<string> MesesReceita { get; set; } = new List<string>();

    public decimal TotalNotasEmitidas { get; set; }
    public decimal TotalNotasSemCobranca { get; set; }
    public decimal TotalNotasVencidas { get; set; }
    public decimal TotalNotasAVencer { get; set; }
    public decimal TotalNotasPagas { get; set; }

    public async Task OnGetAsync()
    {
        var notas = _context.Notas.AsNoTracking();

        TotalNotasEmitidas = notas.Where(x=> x.Status == StatusNota.Emitida).Select(x => x.Valor).Sum();
        TotalNotasSemCobranca = notas.Where(x => x.DataCobranca == null).Select(x => x.Valor).Sum();
        TotalNotasVencidas = notas.Where(x => x.Status == StatusNota.PagamentoEmAtraso).Select(x => x.Valor).Sum();
        TotalNotasAVencer = notas.Where(x => x.DataCobranca > DateTime.Now).Select(x => x.Valor).Sum();
        TotalNotasPagas = notas.Where(x => x.Status == StatusNota.PagamentoRealizado).Select(x => x.Valor).Sum();

        var inadimplenciaQuery = await notas
            .Where(n => n.Status == StatusNota.PagamentoEmAtraso)
            .GroupBy(n => new { n.DataCobranca.Value.Year, n.DataCobranca.Value.Month })
            .Select(g => new { Mes = g.Key.Month, Ano = g.Key.Year, Count = g.Count() })
            .ToListAsync();

        MesesInadimplencia = inadimplenciaQuery.Select(i => new DateTime(i.Ano, i.Mes, 1).ToString("MMMM yyyy")).ToList();
        InadimplenciaMensal = inadimplenciaQuery.Select(i => i.Count).ToList();


        var receitaQuery = await notas
            .Where(n => n.Status == StatusNota.PagamentoRealizado)
            .GroupBy(n => new { n.DataCobranca.Value.Year, n.DataCobranca.Value.Month })
            .Select(g => new { Mes = g.Key.Month, Ano = g.Key.Year, TotalReceita = g.Sum(n => n.Valor) })
        .ToListAsync();

        MesesReceita = receitaQuery.Select(i => new DateTime(i.Ano, i.Mes, 1).ToString("MMMM yyyy")).ToList();
        ReceitaMensal = receitaQuery.Select(i => i.TotalReceita).ToList();
    }
}
