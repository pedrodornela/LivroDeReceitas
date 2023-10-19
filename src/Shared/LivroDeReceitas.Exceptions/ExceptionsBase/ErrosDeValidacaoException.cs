namespace LivroDeReceitas.Exceptions.ExceptionsBase;

public class ErrosDeValidacaoException : LivroDeReceitasExceptions
{
    public List<string> MensagensDeErro {  get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro) 
    {
        MensagensDeErro = mensagensDeErro;
    }  
}
