using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroBibliotecaController : ControllerBase
    {
        private readonly ILivroBibliotecaRepository _livrobibliotecaRepository;

        private readonly IMapper _mapper;

        public LivroBibliotecaController(ILivroBibliotecaRepository livrobibliotecaRepository, IMapper mapper)
        {
            _livrobibliotecaRepository = livrobibliotecaRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroBiblioteca>>> GetLivroBibliotecaes()
        {
            var livrobibliotecaes = await _livrobibliotecaRepository.SelecionarTodos();
            var livrobibliotecaesDTO = _mapper.Map<IEnumerable<LivroBibliotecaResponseDTO>>(livrobibliotecaes);

            return Ok(livrobibliotecaesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarLivroBiblioteca(LivroBibliotecaDTO livrobiblioteca)
        {
            _livrobibliotecaRepository.Incluir(_mapper.Map<LivroBiblioteca>(livrobiblioteca));
            if (await _livrobibliotecaRepository.SaveAllAsync())
            {
                return Ok(livrobiblioteca);
            }

            return BadRequest("Erro ao salvar livrobiblioteca");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarLivroBiblioteca(int id, LivroBibliotecaDTO livrobiblioteca)
        {
            var livrobibliotecaUpdate = await _livrobibliotecaRepository.SelecionarPorId(id);

            livrobibliotecaUpdate.IdBiblioteca = livrobiblioteca.IdBiblioteca;
            livrobibliotecaUpdate.IdLivro = livrobiblioteca.IdLivro;
            livrobibliotecaUpdate.Quantidade = livrobiblioteca.Quantidade;

            _livrobibliotecaRepository.Alterar(livrobibliotecaUpdate);
            if (await _livrobibliotecaRepository.SaveAllAsync())
            {
                return Ok(livrobibliotecaUpdate);
            }

            return BadRequest("Erro ao alterar livrobiblioteca");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirLivroBiblioteca(int id)
        {

            var livrobiblioteca = await _livrobibliotecaRepository.SelecionarPorId(id);

            _livrobibliotecaRepository.Excluir(livrobiblioteca);
            if (await _livrobibliotecaRepository.SaveAllAsync())
            {
                return Ok("LivroBiblioteca excluido com sucesso");
            }

            return BadRequest("Erro ao excluir livrobiblioteca");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var livrobiblioteca = await _livrobibliotecaRepository.SelecionarPorId(id);
            if (livrobiblioteca == null)
            {
                return NotFound("LivroBiblioteca não encontrado");
            }
            return Ok(_mapper.Map<LivroBibliotecaResponseDTO>(livrobiblioteca));
        }
    }
}
