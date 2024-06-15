﻿using LibraryC.Models;

namespace LibraryC.Interfaces
{
    public interface ILivroBibliotecaRepository
    {
        void Incluir(LivroBiblioteca livrobiblioteca);

        void Alterar(LivroBiblioteca livrobiblioteca);

        void Excluir(LivroBiblioteca livrobiblioteca);

        Task<LivroBiblioteca> SelecionarPorId(int id);

        Task<IEnumerable<LivroBiblioteca>> SelecionarTodos();

        Task<LivroBiblioteca> LivroBibliotecaPorIdLivroEIdBiblioteca(int idLivro, int idBiblioteca);

        void DiminuirQuantidadeLivro(LivroBiblioteca livroBiblioteca);

        Task<bool> SaveAllAsync();
    }
}
