namespace LivroDeReceitas.Exceptions.ExceptionsBase;

public class LoginInvalidoException : LivroDeReceitasExceptions
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO) 
    {

    }

}
