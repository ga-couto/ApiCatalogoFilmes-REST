using System;

namespace ApiCatalogoFilmes.ViewModel
{
    public class FilmeViewModel
    {
        public Guid ID {get; set;}
        public string NOME { get; set; }
        public string PRODUTORA { get; set; }
        public double PRECO { get; set; }
        public string SINOPSE { get; set; }

    }
}
