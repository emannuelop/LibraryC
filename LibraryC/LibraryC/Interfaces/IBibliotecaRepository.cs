using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface IBibliotecaRepository
    {
        void Incluir(Biblioteca biblioteca);

        void Alterar(Biblioteca biblioteca);

        void Excluir(Biblioteca biblioteca);

        Task<Biblioteca> SelecionarPorId(int id);

        Task<IEnumerable<Biblioteca>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
