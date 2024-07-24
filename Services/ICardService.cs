using CARDGENERATOR.DTOs.RequestModels;
using CARDGENERATOR.DTOs.ResponseModels;
using CARDGENERATOR.Models;

namespace CARDGENERATOR.Services
{
    public interface ICardService
    {
        Task<BaseResponse> UseCard(UseCardRequestModel model);
        Task<BaseResponse<List<CardResponseModel>>> GetAllCards();
        Task<BaseResponse<CreationResponseDto>> GenerateCard(GenerateCardRequestModel model);
        Task<BaseResponse<PurchaseCardResponseModel>> PurchaseCard(PurchaseCardRequestModel model);
    }
}
