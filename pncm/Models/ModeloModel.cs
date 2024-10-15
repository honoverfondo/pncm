using System.ComponentModel.DataAnnotations;

namespace pncm.Models
{
    public class ModeloModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Obrigatorio!")]
        public string Nome { get; set; }

        public DateTime DataReg { get; set; } = DateTime.Now;
    }
}
