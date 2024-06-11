using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class AutorRepository : IAutorRepository
    {

        private readonly LibrarycContext _context;

        public AutorRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Autor autor)
        {
            _context.Entry(autor).State = EntityState.Modified;
        }

        public void Excluir(Autor autor)
        {
            _context.Autor.Remove(autor);
        }

        public void Incluir(Autor autor)
        {
            _context.Autor.Add(autor);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<Autor> SelecionarPorId(int id)
        {
            var autor = await _context.Autor.Where(x=> x.IdAutor == id).FirstOrDefaultAsync();
            return autor;
        }

        public async Task<IEnumerable<Autor>> SelecionarTodos()
        {
            return await _context.Autor.ToListAsync();
        }
    }
}
