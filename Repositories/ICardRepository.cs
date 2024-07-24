using CARDGENERATOR.Models;

namespace CARDGENERATOR.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAllCards();
        Task<Card> Create(Card card);
        Task<Card> Update(Card card);
        Task<Card> GetCard(Guid Id);
        Task<Card> GetBy(string serialNo);
        Card SearchCard(string searchTerm);
    }
}
