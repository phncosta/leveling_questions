using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<bool> InserirMovimentacaoContaCorrenteAsync(Movimento movimento)
        {
            const string sql = @"INSERT INTO 
                                    movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                                 VALUES 
                                    (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

            using var _dbConn = new SqliteConnection(_databaseConfig.Name);

            return await _dbConn.ExecuteAsync(sql, new
            {
                movimento.IdMovimento,
                movimento.IdContaCorrente,
                movimento.DataMovimento,
                TipoMovimento = movimento.TipoMovimento.ToString(),
                movimento.Valor
            }) > 0;
        }
    }

}
