using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class BibliotecaRepository : IBibliotecaRepository
    {
        private readonly LibrarycContext _context;

        public BibliotecaRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Biblioteca biblioteca)
        {
            _context.Entry(biblioteca).State = EntityState.Modified;
        }

        public void Excluir(Biblioteca biblioteca)
        {
            _context.Biblioteca.Remove(biblioteca);
        }

        public void Incluir(Biblioteca biblioteca)
        {
            _context.Biblioteca.Add(biblioteca);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Biblioteca> SelecionarPorId(int id)
        {
            var biblioteca = await _context.Biblioteca.Where(x => x.IdBiblioteca == id).FirstOrDefaultAsync();
            return biblioteca;
        }

        public async Task<IEnumerable<Biblioteca>> SelecionarTodos()
        {
            return await _context.Biblioteca.ToListAsync();
        }
    }
}
