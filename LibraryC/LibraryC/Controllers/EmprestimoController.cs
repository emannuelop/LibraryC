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
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoRepository _emprestimoRepository;

        private readonly ILivroBibliotecaRepository _livroBibliotecaRepository;

        private readonly IMapper _mapper;

        public EmprestimoController(IEmprestimoRepository emprestimoRepository, IMapper mapper, ILivroBibliotecaRepository livroBibliotecaRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _mapper = mapper;
            _livroBibliotecaRepository = livroBibliotecaRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimoes()
        {
            var emprestimoes = await _emprestimoRepository.SelecionarTodos();
            var emprestimoesDTO = _mapper.Map<IEnumerable<EmprestimoResponseDTO>>(emprestimoes);

            return Ok(emprestimoesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarEmprestimo(EmprestimoDTO emprestimo)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            var livroBiblioteca = await _livroBibliotecaRepository.LivroBibliotecaPorIdLivroEIdBiblioteca(emprestimo.IdLivro,1);

            Emprestimo newEmprestimo = new Emprestimo();

            newEmprestimo.IdLivro = emprestimo.IdLivro;
            newEmprestimo.IdCliente = emprestimo.IdCliente;
            newEmprestimo.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;
            newEmprestimo.DataEmprestimo = dataAtual;
            newEmprestimo.Status = "Nao devolvido";




            _emprestimoRepository.Incluir(newEmprestimo);
            _livroBibliotecaRepository.DiminuirQuantidadeLivro(livroBiblioteca);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok("Emprestimo cadastrado com sucesso");
            }

            return BadRequest("Erro ao salvar emprestimo");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarEmprestimo(int id, EmprestimoDTO emprestimo)
        {
            var emprestimoUpdate = await _emprestimoRepository.SelecionarPorId(id);

            emprestimoUpdate.IdCliente = emprestimo.IdCliente;
            emprestimoUpdate.IdLivro = emprestimo.IdLivro;
            emprestimoUpdate.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;

            _emprestimoRepository.Alterar(emprestimoUpdate);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok("Emprestimo alterado com sucesso");
            }

            return BadRequest("Erro ao alterar emprestimo");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirEmprestimo(int id)
        {

            var emprestimo = await _emprestimoRepository.SelecionarPorId(id);

            _emprestimoRepository.Excluir(emprestimo);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok("Emprestimo excluido com sucesso");
            }

            return BadRequest("Erro ao excluir emprestimo");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var emprestimo = await _emprestimoRepository.SelecionarPorId(id);
            if (emprestimo == null)
            {
                return NotFound("Emprestimo não encontrado");
            }
            return Ok(_mapper.Map<EmprestimoResponseDTO>(emprestimo));
        }
    }
}
