using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface IAutorRepository
    {
        void Incluir(Autor autor);

        void Alterar(Autor autor);

        void Excluir(Autor autor);

        Task<Autor> SelecionarPorId(int id);
        
        Task<IEnumerable<Autor>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
