using System;


namespace ApiCatalogoFilmes.Entities
{
    public class Filme
    {
        public Guid ID { get; set; }
        public string NOME { get; set; }
        public string PRODUTORA { get; set; }
        public double PRECO { get; set; }
        public string SINOPSE { get; set; }        
        
    }
}
