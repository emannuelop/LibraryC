using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibrarycContext _context;

        public UsuarioRepository(LibrarycContext context)
        {
            _context = context;
        }

        public void Alterar(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }

        public void Excluir(Usuario usuario)
        {
            _context.Usuario.Remove(usuario);
        }

        public void Incluir(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario> SelecionarPorId(int id)
        {
            var usuario = await _context.Usuario.Where(x => x.IdUsuario == id).FirstOrDefaultAsync();
            return usuario;
        }

        public Usuario SelecionarPorEmail(string email)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> SelecionarTodos()
        {
            return await _context.Usuario.ToListAsync();
        }
    }
}
