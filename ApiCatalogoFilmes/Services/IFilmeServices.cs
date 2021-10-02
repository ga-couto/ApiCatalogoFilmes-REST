using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Services
{
    public interface IFilmeServices : IDisposable
    {
        Task<List<FilmeViewModel>> Obter(int pagina, int quantidade);
        Task<FilmeViewModel> Obter(Guid id);
        Task<FilmeViewModel> Inserir(FilmeInputModel filme);
        Task Atualizar(Guid id, FilmeInputModel filme);
        Task Atualizar(Guid id, double preco);
        Task Atualzar(Guid id, string sinopse);
        Task Deletar(Guid id);
        Task Atualizar(Guid idFilme, string sinopse);
    } 
}
