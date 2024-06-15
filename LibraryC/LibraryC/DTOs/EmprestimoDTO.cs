namespace LibraryC.DTOs
{
    public class EmprestimoDTO
    {

        public int IdCliente { get; set; }

        public int IdLivro { get; set; }

        public DateOnly DataPrevistaDevolucao { get; set; }
    }
}
