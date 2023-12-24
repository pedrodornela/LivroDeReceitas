using LivroDeReceitas.Comunicacao.Enum;

namespace LivroDeReceitas.Comunicacao.Request;
public class RequisicaoDashboardJson
{
    public string TituloOuIngrediente { get; set; }
    public Categoria? Categoria { get; set; }
}
