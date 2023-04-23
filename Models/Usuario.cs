using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace WIS_PI.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Senha { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public virtual Genero? Genero { get; set; }
        public int GeneroId { get; set; }
    }
}
