using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrente> ObterContaCorrentePorIdAsync(string idContaCorrente)
        {
            const string sql = @"SELECT 
                                    idcontacorrente,
                                    numero,
                                    nome,
                                    ativo
                                 FROM contacorrente 
                                 WHERE idcontacorrente = @IdContaCorrente";

            using var _dbConn = new SqliteConnection(_databaseConfig.Name);

            return await _dbConn.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { IdContaCorrente = idContaCorrente });
        }

        public async Task<decimal> ObterSaldoAsync(string idContaCorrente)
        {
            const string sql = @"SELECT 
                                    COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END), 0) -
                                    COALESCE(SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END), 0)
                                 FROM 
                                    movimento
                                 WHERE 
                                    idcontacorrente = @IdContaCorrente;";

            using var _dbConn = new SqliteConnection(_databaseConfig.Name);

            return await _dbConn.QueryFirstOrDefaultAsync<decimal>(sql, new { IdContaCorrente = idContaCorrente });
        }
    }
}
