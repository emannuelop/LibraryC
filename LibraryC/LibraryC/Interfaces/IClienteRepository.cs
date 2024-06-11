using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface IClienteRepository
    {
        void Incluir(Cliente cliente);

        void Alterar(Cliente cliente);

        void Excluir(Cliente cliente);

        Task<Cliente> SelecionarPorId(int id);

        Task<IEnumerable<Cliente>> SelecionarTodos();

        Task<bool> SaveAllAsync();
    }
}
