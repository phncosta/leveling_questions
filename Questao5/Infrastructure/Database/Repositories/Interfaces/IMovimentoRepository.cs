﻿using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<bool> InserirMovimentacaoContaCorrenteAsync(Movimento movimento);
    }
}
