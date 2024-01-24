using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteRodonaves.Models
{
    [Table("Colaboradores")]
    public class Colaborador
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public Unidade Unidade { get; set; }

        [ForeignKey("Unidade")]
        public int Unidade_IdFK { get; set; }

        public Usuario Usuario { get; set; }

        [ForeignKey("Usuario")]
        public int Usuario_IdFK { get; set; }
    }
}
