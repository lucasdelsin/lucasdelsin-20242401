using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteRodonaves.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }
        
        public string Senha { get; set; }
        
        public bool Status { get; set; }
    }
}