using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stefanini.Domain.Models
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ser uma string com máximo de 200 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "O campo {0} precisa ser uma string com 2 caracteres")]
        public string UF { get; set; }
        public Pessoa? pessoa { get; set; }
    }
}
