using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stefanini.Domain.Models
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ser uma string com máximo de 300 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ser uma string com máximo de 200 caracteres")]
        public string CPF { get; set; }
        [ForeignKey("Cidade_FK")]
        public int Id_Cidade { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor digite uma idade válida")]
        public int Idade { get; set; }
        public Cidade? cidade { get; set; }
    }
}
