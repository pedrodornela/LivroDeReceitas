using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace LivroDeReceitas.Domain.Extension;
public static class StringExtension
{
    public static bool Compara_UpperCase_E_Acentos(this string origem, string pesquisarPor)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(origem, pesquisarPor,
                                CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);

        return index >= 0;

    }

    public static string RemoverAcentos(this string texto)
    {
        return new string(texto.Normalize(NormalizationForm.FormD)
            .Where(ch => char.GetUnicodeCategory(ch) !=  UnicodeCategory.NonSpacingMark).ToString());
    }
}
