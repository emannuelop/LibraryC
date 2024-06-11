using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioes()
        {
            var usuarioes = await _usuarioRepository.SelecionarTodos();
            var usuarioesDTO = _mapper.Map<IEnumerable<UsuarioResponseDTO>>(usuarioes);

            return Ok(usuarioesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(UsuarioDTO usuario)
        {
            _usuarioRepository.Incluir(_mapper.Map<Usuario>(usuario));
            if (await _usuarioRepository.SaveAllAsync())
            {
                return Ok("Usuario cadastrado com sucesso");
            }

            return BadRequest("Erro ao salvar usuario");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarUsuario(int id, UsuarioDTO usuario)
        {
            var usuarioUpdate = await _usuarioRepository.SelecionarPorId(id);

            usuarioUpdate.Nome = usuario.Nome;
            usuarioUpdate.Cpf = usuario.Cpf;
            usuarioUpdate.Perfil = usuario.Perfil;
            usuarioUpdate.Senha = usuario.Senha;
            usuarioUpdate.Email = usuario.Email;

            _usuarioRepository.Alterar(usuarioUpdate);
            if (await _usuarioRepository.SaveAllAsync())
            {
                return Ok("Usuario alterado com sucesso");
            }

            return BadRequest("Erro ao alterar usuario");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirUsuario(int id)
        {

            var usuario = await _usuarioRepository.SelecionarPorId(id);

            _usuarioRepository.Excluir(usuario);
            if (await _usuarioRepository.SaveAllAsync())
            {
                return Ok("Usuario excluido com sucesso");
            }

            return BadRequest("Erro ao excluir usuario");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var usuario = await _usuarioRepository.SelecionarPorId(id);
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado");
            }
            return Ok(_mapper.Map<UsuarioResponseDTO>(usuario));
        }
    }
}
