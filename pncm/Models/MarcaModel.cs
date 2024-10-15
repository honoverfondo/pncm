using System.ComponentModel.DataAnnotations;

namespace pncm.Models
{
    public class MarcaModel
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Porfavor Digite a Marca do equipamento!")]
        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }= DateTime.Now;
    }
}
