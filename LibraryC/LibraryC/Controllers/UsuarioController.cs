﻿using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IMapper _mapper;

        private readonly ITokenService _tokenService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
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

            if (usuario.Perfil == "admin"|| usuario.Perfil == "funcionario")
            {

                Usuario usuarioUpdate = new Usuario();

                usuarioUpdate.Nome = usuario.Nome;
                usuarioUpdate.Cpf = usuario.Cpf;
                usuarioUpdate.Perfil = usuario.Perfil;
                usuarioUpdate.Senha = _tokenService.HashPassword(usuario.Senha);
                usuarioUpdate.Email = usuario.Email;

                _usuarioRepository.Incluir(usuarioUpdate);
                if (await _usuarioRepository.SaveAllAsync())
                {
                    return Ok(usuarioUpdate);
                }
            }

            return BadRequest("Erro ao salvar usuario");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarUsuario(int id, UsuarioDTO usuario)
        {

            if (usuario.Perfil.Equals("admin") || usuario.Perfil.Equals("funcionario"))
            {

                var usuarioUpdate = await _usuarioRepository.SelecionarPorId(id);

                usuarioUpdate.Nome = usuario.Nome;
                usuarioUpdate.Cpf = usuario.Cpf;
                usuarioUpdate.Perfil = usuario.Perfil;
                usuarioUpdate.Email = usuario.Email;

                _usuarioRepository.Alterar(usuarioUpdate);
                if (await _usuarioRepository.SaveAllAsync())
                {
                    return Ok(usuarioUpdate);
                }
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
                return Ok(id);
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

        [HttpGet("roles")]
        public async Task<ActionResult<List<String>>> GetPerfis()
        {
            List<string> listaDeStrings = new List<string>();

            // Adicionando elementos à lista
            listaDeStrings.Add("admin");
            listaDeStrings.Add("funcionario");

            return Ok(listaDeStrings);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userEmail == null)
            {
                return Unauthorized();
            }

            var user = _usuarioRepository.SelecionarPorEmail(userEmail);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.IdUsuario,
                user.Email,
                user.Cpf
            });
        }

        [HttpPatch("alterar-senha")]
        public async Task<IActionResult> AlterarSenha(SenhaDTO senhaDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userEmail == null)
            {
                return Unauthorized();
            }

            var user = _usuarioRepository.SelecionarPorEmail(userEmail);

            if (user == null || !user.Senha.Equals(_tokenService.HashPassword(senhaDto.SenhaAtual)))
            {
                return NotFound();
            }

            user.Senha = _tokenService.HashPassword(senhaDto.SenhaNova);

            _usuarioRepository.Alterar(user);

            await _usuarioRepository.SaveAllAsync();

            return Ok(new
            {
                user.IdUsuario,
                user.Email,
                user.Cpf
            });
        }

    }
}
