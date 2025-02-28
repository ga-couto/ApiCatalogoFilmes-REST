﻿using ApiCatalogoFilmes.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public interface IFilmeRepositorio : IDisposable
    {   
        Task<List<Filme>> Obter(int pagina, int quantidade);
        Task<Filme> Obter(Guid id);
        Task<List<Filme>> Obter(string nome, string produtora);
        Task Inserir(Filme filme);
        Task Atualizar(Filme filme);
        Task Deletar(Guid id);        
    }
}
