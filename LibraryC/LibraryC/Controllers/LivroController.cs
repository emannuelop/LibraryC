﻿using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivroes()
        {
            var livroes = await _livroRepository.SelecionarTodos();
            var livroesDTO = _mapper.Map<IEnumerable<LivroResponseDTO>>(livroes);

            return Ok(livroesDTO);
        }

        [HttpGet("livrosByTitulo/{filtro}")]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivrosByTitulo(String filtro)
        {
            var livroes = await _livroRepository.SelecionarTodos();
            var livroesDTO = _mapper.Map<IEnumerable<LivroResponseDTO>>(livroes);

            return Ok(livroesDTO.Where(u => u.Titulo.Contains(filtro)));
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarLivro(LivroDTO livro)
        {
            _livroRepository.Incluir(_mapper.Map<Livro>(livro));
            if (await _livroRepository.SaveAllAsync())
            {
                return Ok(livro);
            }

            return BadRequest("Erro ao salvar livro");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarLivro(int id, LivroDTO livro)
        {
            var livroUpdate = await _livroRepository.SelecionarPorId(id);

            livroUpdate.Titulo = livro.Titulo;
            livroUpdate.AnoPublicacao = livro.AnoPublicacao;
            livroUpdate.IdAutor = livro.IdAutor;
            livroUpdate.Quantidade = livro.Quantidade;

            _livroRepository.Alterar(livroUpdate);
            if (await _livroRepository.SaveAllAsync())
            {
                return Ok(livroUpdate);
            }

            return BadRequest("Erro ao alterar livro");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirLivro(int id)
        {

            var livro = await _livroRepository.SelecionarPorId(id);

            _livroRepository.Excluir(livro);
            if (await _livroRepository.SaveAllAsync())
            {
                return Ok(id);
            }

            return BadRequest("Erro ao excluir livro");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var livro = await _livroRepository.SelecionarPorId(id);
            if (livro == null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(_mapper.Map<LivroResponseDTO>(livro));
        }
    }
}
