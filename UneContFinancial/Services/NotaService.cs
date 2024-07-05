using Microsoft.EntityFrameworkCore;
using UneContFinancial.Data;

namespace UneContFinancial.Services;

public class NotaService
{
    const string folderPath = "wwwroot/uploads/notas";

    internal static bool ValidaSeExisteNumeroIdentificacao(AppDbContext context, string numeroIdentificacao)
    {
        return context.Notas.AsNoTracking().Where(x => x.NumeroIdentificacao == numeroIdentificacao).Any();
    }

    internal static bool ValidaSeNotaFiscalFoiSelecionado(IFormFile notaFiscal)
    {
        return notaFiscal == null ? false : true;
    }

    internal static bool ValidaSeBoletolFoiSelecionado(IFormFile boletoBancario)
    {
        return boletoBancario == null ? false : true;
    }

    internal static string SalvarNotaFiscal(IFormFile NotaFiscal)
    {
        if (!UploadService.ValidaExtensaoArquivo(Path.GetExtension(NotaFiscal.FileName))) throw new Exception("Extensão do arquivo (NotaFiscal) inválida!");

        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string fileName = UploadService.MakeUniqueFilename(folderPath, NotaFiscal.FileName);

        var pathNotaFiscal = Path.Combine(folderPath, fileName);
        using var stream = new FileStream(pathNotaFiscal, FileMode.Create);
        NotaFiscal.CopyTo(stream);

        return fileName;
    }

    internal static string SalvarBoletoBancario(IFormFile BoletoBancario)
    {
        if (!UploadService.ValidaExtensaoArquivo(Path.GetExtension(BoletoBancario.FileName))) throw new Exception("Extensão do arquivo(Boleto Bancario) inválida!");

        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        string fileName = UploadService.MakeUniqueFilename(folderPath, BoletoBancario.FileName);

        var pathBoleto = Path.Combine(folderPath, fileName);
        using var stream = new FileStream(pathBoleto, FileMode.Create);
        BoletoBancario.CopyTo(stream);

        return fileName;
    }
}