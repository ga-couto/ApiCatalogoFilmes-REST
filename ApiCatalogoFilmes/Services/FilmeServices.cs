using ApiCatalogoFilmes.Entities;
using ApiCatalogoFilmes.Exceptions;
using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.Repositorios;
using ApiCatalogoFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Services
{
    public class FilmeServices : IFilmeServices
    {

        private readonly IFilmeRepositorio _filmeRepositorio;

        public FilmeServices(IFilmeRepositorio filmeRepositorio)
        {
            _filmeRepositorio = filmeRepositorio;
        }


        public async Task<List<FilmeViewModel>> Obter(int pagina, int quantidade)
        {
            var filmes = await _filmeRepositorio.Obter(pagina, quantidade);

            return filmes.Select(filme => new FilmeViewModel
            {
                ID = filme.ID,
                NOME = filme.NOME,
                PRODUTORA = filme.PRODUTORA,
                PRECO = filme.PRECO,
                SINOPSE = filme.SINOPSE
            })
                               .ToList();
        }


        public async Task<FilmeViewModel> Obter(Guid id)
        {
            var filme = await _filmeRepositorio.Obter(id);

            if (filme == null)
                return null;

            return new FilmeViewModel
            {
                ID = filme.ID,
                NOME = filme.NOME,
                PRODUTORA = filme.PRODUTORA,
                PRECO = filme.PRECO,
                SINOPSE = filme.SINOPSE
            };
        }


        public async Task<FilmeViewModel> Inserir(FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(filme.NOME, filme.PRODUTORA);

            if (entidadeFilme.Count > 0)
                throw new FilmeJaCadastradoException();

            var filmeInsert = new Filme
            {
                ID = Guid.NewGuid(),
                NOME = filme.NOME,
                PRODUTORA = filme.PRODUTORA,
                PRECO = filme.PRECO,
                SINOPSE = filme.SINOPSE
            };

            await _filmeRepositorio.Inserir(filmeInsert);

            return new FilmeViewModel
            {
                ID = filmeInsert.ID,
                NOME = filme.NOME,
                PRODUTORA = filme.PRODUTORA,
                PRECO = filme.PRECO,
                SINOPSE = filme.SINOPSE
            };
        }


        public async Task Atualizar(Guid id, FilmeInputModel filme)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(id);

            if (entidadeFilme == null)
                throw new FilmeNaoCadastradoException();

            entidadeFilme.NOME = filme.NOME;
            entidadeFilme.PRODUTORA = filme.PRODUTORA;
            entidadeFilme.PRECO = filme.PRECO;
            entidadeFilme.SINOPSE = filme.SINOPSE;

            await _filmeRepositorio.Atualizar(entidadeFilme);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeFilme = await _filmeRepositorio.Obter(id);

            if (entidadeFilme == null)
                throw new FilmeNaoCadastradoException();

            entidadeFilme.PRECO = preco;

            await _filmeRepositorio.Atualizar(entidadeFilme);
        }

        public async Task Deletar(Guid id)
        {
            var filme = await _filmeRepositorio.Obter(id);

            if (filme == null)
                throw new FilmeNaoCadastradoException();

            await _filmeRepositorio.Deletar(id);
        }

        public void Dispose()
        {
            _filmeRepositorio?.Dispose();
        }

        public Task Atualzar(Guid id, string sinopse)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Guid idFilme, string sinopse)
        {
            throw new NotImplementedException();
        }
    }
}
