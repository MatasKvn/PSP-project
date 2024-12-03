using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [ApiController]
    [Route("api/business-details")]
    public class BusinessDetailController(IBusinessDetailService _businessDetailService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBusinessDetails(CancellationToken cancellationToken)
        {
            var businessDetailsDto = await _businessDetailService.GetBusinessDetailsAsync(cancellationToken);
            return Ok(businessDetailsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusinessDetails([FromBody] BusinessDetailsRequest businessDetailsDto, CancellationToken cancellationToken)
        {
            var businessDetailsResponseDto = await _businessDetailService.CreateOrUpdateBusinessDetailsAsync(businessDetailsDto, cancellationToken);
            return Ok(businessDetailsResponseDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBusinessDetails([FromBody] BusinessDetailsRequest businessDetailsDto, CancellationToken cancellationToken)
        {
            var businessDetailsResponseDto = await _businessDetailService.CreateOrUpdateBusinessDetailsAsync(businessDetailsDto, cancellationToken);
            return Ok(businessDetailsResponseDto);
        }
    }
}
