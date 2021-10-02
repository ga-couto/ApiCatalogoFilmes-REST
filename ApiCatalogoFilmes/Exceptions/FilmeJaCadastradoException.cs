using System;


namespace ApiCatalogoFilmes.Exceptions
{
    public class FilmeJaCadastradoException : Exception
    {
        public FilmeJaCadastradoException()
            : base("Filme já existente em nosso repositório!")
        { 

        }
    }
}