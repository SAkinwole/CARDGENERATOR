using CARDGENERATOR.Data;
using CARDGENERATOR.Models;
using Microsoft.EntityFrameworkCore;

namespace CARDGENERATOR.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            return await _context.Cards.ToListAsync();

        }
        public async Task<Card> Create(Card card)
        {
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();
            return card;
        }
        public async Task<Card> Update(Card card)
        {
            _context.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> GetCard(Guid Id)
        {
            var result = await _context.Cards.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<Card> GetBy(string serialNo)
        {
            var result = await _context.Cards.Where(x => x.SerialNo == serialNo).FirstOrDefaultAsync();
            return result;
        }
        public Card SearchCard(string searchTerm)
        {
            var result = _context.Cards.Where(x => x.PIN == searchTerm || x.SerialNo == searchTerm
                                                    || x.ExamNo == searchTerm).FirstOrDefault();
            return result;
        }
    }
}
