using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UneContFinancial.Data;
using UneContFinancial.Models;
using UneContFinancial.Services;

namespace UneContFinancial.Pages.Notas
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Nota Nota { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(IFormFile NotaFiscal, IFormFile BoletoBancario)
        {
            try
            {
                if (NotaService.ValidaSeExisteNumeroIdentificacao(_context, Nota.NumeroIdentificacao)) throw new Exception("Já existe um nota cadastrada com esse numero de identificação!");

                if (NotaService.ValidaSeNotaFiscalFoiSelecionado(NotaFiscal)) Nota.NotaFiscal = NotaService.SalvarNotaFiscal(NotaFiscal);
                if (NotaService.ValidaSeBoletolFoiSelecionado(BoletoBancario)) Nota.BoletoBancario = NotaService.SalvarBoletoBancario(BoletoBancario);

                _context.Notas.Add(Nota);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
