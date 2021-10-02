using ApiCatalogoFilmes.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiCatalogoFilmes.Repositorios
{
    public class FilmeSqlServerRepository : IFilmeRepositorio
    {
        private readonly SqlConnection sqlConnection;

        public FilmeSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Filmes order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    ID = (Guid)sqlDataReader["Id"],
                    NOME = (string)sqlDataReader["Nome"],
                    PRODUTORA = (string)sqlDataReader["Produtora"],
                    PRECO = (double)sqlDataReader["Preco"],
                    SINOPSE = (string)sqlDataReader["Sinopse"]
                });
            }

            await sqlConnection.CloseAsync();

            return filmes;
        }

        public async Task<Filme> Obter(Guid id)
        {
            Filme filme = null;

            var comando = $"select * from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filme = new Filme
                {
                    ID = (Guid)sqlDataReader["Id"],
                    NOME = (string)sqlDataReader["Nome"],
                    PRODUTORA = (string)sqlDataReader["Produtora"],
                    PRECO = (double)sqlDataReader["Preco"],
                    SINOPSE = (string)sqlDataReader["Sinopse"]
                };
            }

            await sqlConnection.CloseAsync();

            return filme;
        }

        public async Task<List<Filme>> Obter(string nome, string produtora)
        {
            var filmes = new List<Filme>();

            var comando = $"select * from Filmes where Nome = '{nome}' and Produtora = '{produtora}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                filmes.Add(new Filme
                {
                    ID = (Guid)sqlDataReader["Id"],
                    NOME = (string)sqlDataReader["Nome"],
                    PRODUTORA = (string)sqlDataReader["Produtora"],
                    PRECO = (double)sqlDataReader["Preco"],
                    SINOPSE = (string)sqlDataReader["Sinopse"]
                });
            }

            await sqlConnection.CloseAsync();

            return filmes;
        }

        public async Task Inserir(Filme filme)
        {
            var comando = $"insert Filmes (Id, Nome, Produtora, Preco, Sinopse) values ('{filme.ID}', '{filme.NOME}', '{filme.PRODUTORA}', {filme.PRECO.ToString().Replace(",", ".")}, '{filme.SINOPSE}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Filme filme)
        {
            var comando = $"update Filmes set Nome = '{filme.NOME}', Produtora = '{filme.PRODUTORA}', Preco = {filme.PRECO.ToString().Replace(",", ".")}, '{filme.SINOPSE}' where Id = '{filme.ID}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Deletar(Guid id)
        {
            var comando = $"delete from Filmes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
