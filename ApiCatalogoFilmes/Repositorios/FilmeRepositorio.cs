using ApiCatalogoFilmes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public class FilmeRepositorio : IFilmeRepositorio
    {
        private static Dictionary<Guid, Filme> filmes = new Dictionary<Guid, Filme>()
        {
            {Guid.Parse("aaaaa-aaaaaaaaaa-aaaaaaaaaa"), new Filme{ ID = Guid.Parse("aaaaa-aaaaaaaaaa-aaaaaaaaaa"), NOME = "Primeira semana", PRODUTORA = "Sony Pictures Entertainment.", PRECO = 200, SINOPSE = "As vezes nem todo sábado vem depois da sexta-feira."} },
            {Guid.Parse("bbbbb-bbbbbbbbbb-bbbbbbbbbb"), new Filme{ ID = Guid.Parse("bbbbb-bbbbbbbbbb-bbbbbbbbbb"), NOME = "A jornada Bootcamp", PRODUTORA = "Universal Pictures", PRECO = 190, SINOPSE = "Esse garoto está lutando por uma vida melhor."} },
            {Guid.Parse("ccccc-cccccccccc-cccccccccc"), new Filme{ ID = Guid.Parse("ccccc-cccccccccc-cccccccccc"), NOME = "Fui na frente", PRODUTORA = "The Walt Disney Company", PRECO = 180, SINOPSE = "Quando sobem as escadas, niguém mais abre a porta."} },
            {Guid.Parse("ddddd-dddddddddd-dddddddddd"), new Filme{ ID = Guid.Parse("ddddd-dddddddddd-dddddddddd"), NOME = "Sétimo", PRODUTORA = "Lions Gate Entertainment", PRECO = 170, SINOPSE = "Eram 5, agora todos vão a procura do segundo."} },
            {Guid.Parse("eeeee-eeeeeeeeee-eeeeeeeeee"), new Filme{ ID = Guid.Parse("eeeee-eeeeeeeeee-eeeeeeeeee"), NOME = "Outra vez", PRODUTORA = "The Walt Disney Company", PRECO = 80, SINOPSE = "Dois jovens descobrindo o mundo pela segunda vez."} },
            {Guid.Parse("fffff-ffffffffff-ffffffffff"), new Filme{ ID = Guid.Parse("fffff-ffffffffff-ffffffffff"), NOME = "BR que fala?", PRODUTORA = "Lions Gate Entertainment", PRECO = 190, SINOPSE = "Conta uma história da familia brasileira."} }
        };


        public Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(filmes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Filme> Obter(Guid id)
        {
            if (!filmes.ContainsKey(id))
                return Task.FromResult<Filme>(null);

            return Task.FromResult(filmes[id]);
        }

        public Task<List<Filme>> Obter(string nome, string produtora)
        {
            return Task.FromResult(filmes.Values.Where(filme => filme.NOME.Equals(nome) && filme.PRODUTORA.Equals(produtora)).ToList());
        }

        public Task<List<Filme>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Filme>();

            foreach (var filme in filmes.Values)
            {
                if (filme.NOME.Equals(nome) && filme.PRODUTORA.Equals(produtora))
                    retorno.Add(filme);
            }

            return Task.FromResult(retorno);
        }
        public Task Inserir(Filme filme)
        {
            filmes.Add(filme.ID, filme);
            return Task.CompletedTask;
        }

        public Task Atualizar(Filme filme)
        {
            filmes[filme.ID] = filme;
            return Task.CompletedTask;
        }

        public Task Deletar(Guid id)
        {
            filmes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
           //Encerra conexão com o DB
        }
    }
}
