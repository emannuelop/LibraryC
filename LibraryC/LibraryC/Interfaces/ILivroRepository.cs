using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface ILivroRepository
    {
        void Incluir(Livro livro);

        void Alterar(Livro livro);

        void Excluir(Livro livro);

        Task<Livro> SelecionarPorId(int id);

        Task<IEnumerable<Livro>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
