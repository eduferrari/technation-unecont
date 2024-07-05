namespace UneContFinancial.Services;

public class UploadService
{
    public static string MakeUniqueFilename(string dir, string filename)
    {
        string text = filename;
        int num = 0;
        while (File.Exists(Path.Combine(dir, text)))
        {
            num++;
            text = Path.GetFileNameWithoutExtension(filename) + "-copy-" + num + Path.GetExtension(filename);
        }

        return text.ToLower();
    }

    public static bool ValidaExtensaoArquivo(string extensao)
    {
        string[] source =
        [
            ".jpg", ".jpeg", ".gif", ".bmp", ".png", ".pdf"
        ];

        return source.Contains(extensao);
    }
}
