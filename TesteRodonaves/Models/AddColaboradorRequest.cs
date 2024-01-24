namespace TesteRodonaves.Models
{
    public class AddColaboradorRequest
    {
        public string Nome { get; set; }

        public int Unidade_IdFK { get; set; }

        public int Usuario_IdFK { get; set; }
    }
}
