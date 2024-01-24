using Microsoft.Extensions.Primitives;

namespace TesteRodonaves.Models
{
    public class UpdateUsuarioRequest
    {
        public string Senha { get; set; }

        public bool Status { get; set; }
    }
}
