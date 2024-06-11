using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class LivroBibliotecaRepository: ILivroBibliotecaRepository
    {
        private readonly LibrarycContext _context;

        public LivroBibliotecaRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(LivroBiblioteca livrobiblioteca)
        {
            _context.Entry(livrobiblioteca).State = EntityState.Modified;
        }

        public void Excluir(LivroBiblioteca livrobiblioteca)
        {
            _context.LivroBiblioteca.Remove(livrobiblioteca);
        }

        public void Incluir(LivroBiblioteca livrobiblioteca)
        {
            _context.LivroBiblioteca.Add(livrobiblioteca);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<LivroBiblioteca> SelecionarPorId(int id)
        {
            var livrobiblioteca = await _context.LivroBiblioteca.Where(x => x.IdLivroBiblioteca == id).FirstOrDefaultAsync();
            return livrobiblioteca;
        }

        public async Task<IEnumerable<LivroBiblioteca>> SelecionarTodos()
        {
            return await _context.LivroBiblioteca.ToListAsync();
        }
    }
}
