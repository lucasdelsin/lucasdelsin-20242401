using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteRodonaves.Models
{
    [Table("Unidades")]
    public class Unidade
    {
        [Key]
        public int Id { get; set; }

        public int Cod { get; set; }

        public string Nome { get; set; }

        public bool Status { get; set; }

        public List<Colaborador> Colaboradores { get; set; }
    }
}
