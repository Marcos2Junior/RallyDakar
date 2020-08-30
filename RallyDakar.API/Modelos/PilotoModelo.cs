using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace RallyDakar.API.Modelos
{
    public class PilotoModelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome é preenchimento obrigatorio.")]
        [MaxLength(50, ErrorMessage = "Nome nao pode ter mais que 50 caracteres.")]
        [MinLength(5, ErrorMessage = "Nome deve ter mais de 5 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "SobreNome é preenchimento obrigatorio.")]
        [MaxLength(50, ErrorMessage = "SobreNome nao pode ter mais que 50 caracteres.")]
        [MinLength(5, ErrorMessage = "SobreNome deve ter mais de 5 caracteres")]
        public string SobreNome { get; set; }
        public int EquipeId { get; set; }
        public string NomeCompleto => $"{Nome} {SobreNome}";
    }
}
