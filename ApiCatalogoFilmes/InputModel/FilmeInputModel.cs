using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoFilmes.InputModel
{
    public class FilmeInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O filme deve coter entre 3 e 100 caracteres!")]
        public string NOME { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "A produtora deve coter entre 3 e 100 caracteres!")]
        public string PRODUTORA { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve estar entre 1 e 1000 reais!")]
        public double PRECO { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "A Sinopse do filme deve coter entre 3 e 300 caracteres!")]
        public string SINOPSE { get; set; }    
    }   
}
