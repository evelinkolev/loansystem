﻿using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Infrastructure.Persistence.Repositories
{
    internal sealed class CardRepository : ICardRepository
    {
        private readonly LoanSystemContext _context;
        private readonly DbSet<Card> _cards;

        public CardRepository(LoanSystemContext context)
        {
            _context = context;
            _cards = _context.Cards;
        }

        public async Task CreateAsync(Card card)
        {
            await _cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Card card)
        {
            _cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card?> GetAsync(Guid id)
        {
            return await _cards.Include(x => x.Payer).AsNoTracking().Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> PayerHaveCardAsync(Guid cardId, Guid payerId)
        {
            var card = await GetAsync(cardId);

            if (card is null)
            {
                return false;
            }

            if(card.PayerId != payerId)
            {
                return false;
            }

            return true;
        }
    }
}
