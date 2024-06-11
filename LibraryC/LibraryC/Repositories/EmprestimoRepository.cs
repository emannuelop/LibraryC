using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly LibrarycContext _context;

        public EmprestimoRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Emprestimo emprestimo)
        {
            _context.Entry(emprestimo).State = EntityState.Modified;
        }

        public void Excluir(Emprestimo emprestimo)
        {
            _context.Emprestimo.Remove(emprestimo);
        }

        public void Incluir(Emprestimo emprestimo)
        {
            _context.Emprestimo.Add(emprestimo);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Emprestimo> SelecionarPorId(int id)
        {
            var emprestimo = await _context.Emprestimo.Where(x => x.IdEmprestimo == id).FirstOrDefaultAsync();
            return emprestimo;
        }

        public async Task<IEnumerable<Emprestimo>> SelecionarTodos()
        {
            return await _context.Emprestimo.ToListAsync();
        }
    }
}
