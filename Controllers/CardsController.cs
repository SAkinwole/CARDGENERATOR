using CARDGENERATOR.DTOs.RequestModels;
using CARDGENERATOR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CARDGENERATOR.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        
        [HttpGet("GetAllCards")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _cardService.GetAllCards();
            return Ok(response);
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCard([FromBody] GenerateCardRequestModel model)
        {
            var response = await _cardService.GenerateCard(model);
            if (response.StatusCode == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            return StatusCode(int.Parse(response.StatusCode), response);
          }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseCard([FromBody] PurchaseCardRequestModel model)
        {
            var response = await _cardService.PurchaseCard(model);
            if (response.StatusCode == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            return StatusCode(int.Parse(response.StatusCode), response);
        }
        [HttpPost("useCard")]
        public async Task<IActionResult> UseCard([FromBody] UseCardRequestModel model)
        {
            var response = await _cardService.UseCard(model);
            if (response.StatusCode == StatusCodes.Status200OK.ToString())
            {
                return Ok(response);
            }
            return StatusCode(int.Parse(response.StatusCode), response);
        }
    }
}
