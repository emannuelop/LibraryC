using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class MultaRepository : IMultaRepository
    {
        private readonly LibrarycContext _context;

        public MultaRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Multa multa)
        {
            _context.Entry(multa).State = EntityState.Modified;
        }

        public void Excluir(Multa multa)
        {
            _context.Multa.Remove(multa);
        }

        public void Incluir(Multa multa)
        {
            _context.Multa.Add(multa);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Multa> SelecionarPorId(int id)
        {
            var multa = await _context.Multa.Where(x => x.IdMulta == id).FirstOrDefaultAsync();
            return multa;
        }

        public async Task<IEnumerable<Multa>> SelecionarTodos()
        {
            return await _context.Multa.ToListAsync();
        }
    }
}
