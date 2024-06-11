using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface IMultaRepository
    {
        void Incluir(Multa multa);

        void Alterar(Multa multa);

        void Excluir(Multa multa);

        Task<Multa> SelecionarPorId(int id);

        Task<IEnumerable<Multa>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
