using System.ComponentModel.DataAnnotations;

namespace analyst_challenge.DTO
{
    public class PaginationQueryParameters
    {
        [Required(ErrorMessage = "Parâmetro From é obrigatório")]
        public int From { get; set; }
        [Required(ErrorMessage = "Parâmetro Size é obrigatório")]
        public int Size { get; set; }
        
    }
}
