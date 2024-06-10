using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotenciaRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task InserirIdempotenciaAsync(Idempotencia idempotencia)
        {
            const string sql = @"INSERT INTO 
                                    idempotencia (chave_idempotencia, requisicao, resultado)
                                 VALUES 
                                    (@ChaveIdempotencia, @Requisicao, @Resultado);";

            using var _dbConn = new SqliteConnection(_databaseConfig.Name);
            await _dbConn.ExecuteAsync(sql, idempotencia).ConfigureAwait(false);
        }

        public async Task<Idempotencia> ObterIdempotenciaPorChaveAsync(string chaveIdempotencia)
        {
            const string sql = @"SELECT 
                                    chave_idempotencia as ChaveIdempotencia,
                                    requisicao,
                                    resultado
                                 FROM idempotencia 
                                 WHERE chave_idempotencia = @ChaveIdempotencia";

            using var _dbConn = new SqliteConnection(_databaseConfig.Name);

            return await _dbConn.QueryFirstOrDefaultAsync<Idempotencia>(sql, new { ChaveIdempotencia = chaveIdempotencia }).ConfigureAwait(false);
        }
    }
}
