using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface IEmprestimoRepository
    {
        void Incluir(Emprestimo emprestimo);

        void Alterar(Emprestimo emprestimo);

        void Excluir(Emprestimo emprestimo);

        Task<Emprestimo> SelecionarPorId(int id);

        Task<IEnumerable<Emprestimo>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
