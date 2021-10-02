using System;

namespace ApiCatalogoFilmes.Exceptions
{
    public class FilmeNaoCadastradoException : Exception
    {
        public FilmeNaoCadastradoException()
            : base("Esse filme ainda não existe em nosso repositório")
        {

        }
    }
}