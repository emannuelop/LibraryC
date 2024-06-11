using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LibrarycContext _context;

        public LivroRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Livro livro)
        {
            _context.Entry(livro).State = EntityState.Modified;
        }

        public void Excluir(Livro livro)
        {
            _context.Livro.Remove(livro);
        }

        public void Incluir(Livro livro)
        {
            _context.Livro.Add(livro);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Livro> SelecionarPorId(int id)
        {
            var livro = await _context.Livro.Where(x => x.IdLivro == id).FirstOrDefaultAsync();
            return livro;
        }

        public async Task<IEnumerable<Livro>> SelecionarTodos()
        {
            return await _context.Livro.ToListAsync();
        }
    }
}
