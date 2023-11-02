namespace LivroDeReceitas.Exceptions.ExceptionsBase;

public class LoginInvalidoException : LivroDeReceitasException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO) 
    {

    }

}
