using System.Runtime.Serialization;

namespace LivroDeReceitas.Exceptions.ExceptionsBase;
[Serializable]
public class LoginInvalidoException : LivroDeReceitasException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO) 
    {

    }

    protected LoginInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
