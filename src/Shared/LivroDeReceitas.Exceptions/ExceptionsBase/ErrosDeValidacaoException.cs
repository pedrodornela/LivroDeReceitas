namespace LivroDeReceitas.Exceptions.ExceptionsBase;

public class ErrosDeValidacaoException : LivroDeReceitasException
{
    public List<string> MensagensDeErro {  get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }  
}
