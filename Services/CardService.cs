using CARDGENERATOR.DTOs.RequestModels;
using CARDGENERATOR.DTOs.ResponseModels;
using CARDGENERATOR.Models;
using CARDGENERATOR.Repositories;

namespace CARDGENERATOR.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<BaseResponse<List<CardResponseModel>>> GetAllCards()
        {
            var getCards = await _cardRepository.GetAllCards();
            if (getCards == null)
                return BaseResponse<List<CardResponseModel>>.Failure("Cards not found", StatusCodes.Status404NotFound.ToString());

            var responseModels = getCards.Select(card => new CardResponseModel
            {
                Surname = card.Surname,
                FirstName = card.FirstName,
                LastName = card.LastName,
                DateOfBirth = card.DateOfBirth,
                Sex = card.Sex,
                ExamNo = card.ExamNo,
                SerialNo = card.SerialNo,
                PIN = card.PIN
            }).ToList();

            return BaseResponse<List<CardResponseModel>>.Success("Operation Successful", StatusCodes.Status200OK.ToString(), data: responseModels); 
        } 
        public async Task<BaseResponse<CreationResponseModel>> GenerateCard(GenerateCardRequestModel model)
        {
            var generatedCard = new Card
            {
                Surname = model.Surname,
                FirstName = model.Firstname,
                LastName = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                Sex = model.Sex,
                PIN = GeneratePin(),
                SerialNo = GenerateSerialNo(),
                ExamNo = GenerateExamNo(),
                Status = Status.Generated
            };
            var result = await _cardRepository.Create(generatedCard);


            return BaseResponse<CreationResponseModel>.Success("Operation Successful", StatusCodes.Status200OK.ToString(), new CreationResponseModel { Id = result.Id });
        }

        public async Task<BaseResponse> UseCard(UseCardRequestModel model)
        {
            var getCard = _cardRepository.GetBy(model.SerialNo);
            if (getCard == null)
                return BaseResponse.Failure("Card not found", StatusCodes.Status404NotFound.ToString());

            var generatedCard = new Card
            {
                Status = Status.Used,
                LastUpdateDate = DateTime.UtcNow
            };

            await _cardRepository.Update(generatedCard);

            return BaseResponse.Success("Operation Successful", StatusCodes.Status200OK.ToString());
        }

        public async Task<BaseResponse<PurchaseCardResponseModel>> PurchaseCard(PurchaseCardRequestModel model)
        {
            var getCard = await _cardRepository.GetCard(model.Id);

            if (getCard == null)
            {
                return BaseResponse<PurchaseCardResponseModel>.Failure("No available cards", StatusCodes.Status404NotFound.ToString());
            }

            //PAYMENT IS MADE

            getCard.Status = Status.Purchased;
            getCard.Id = model.Id;


            await _cardRepository.Update(getCard);

            var generatedCard = new PurchaseCardResponseModel
            {
                ExamNo = getCard.ExamNo,
                PIN = getCard.PIN,
                Status = Status.Purchased.ToString(),
                SerialNo = getCard.SerialNo,
            };


            return BaseResponse<PurchaseCardResponseModel>.Success("Operation Successful", StatusCodes.Status200OK.ToString(), data: generatedCard);
        }

        private string GeneratePin()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 16);
        }

        private string GenerateSerialNo()
        {
            return $"WNR{Guid.NewGuid().ToString("N").Substring(0, 10)}";
        }

        private string GenerateExamNo()
        {
            return $"{new Random().Next(10000000, 99999999)}CD";
        }

    }
}
