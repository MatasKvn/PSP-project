using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Services.Interfaces;

namespace POS_System.Api.Controllers
{
    [Route("api/item-discount")]
    [ApiController]
    public class ItemDiscountController(IItemDiscountService _itemDiscountService) : ControllerBase
    {
        [HttpGet]
        [Authorize("ItemDiscountRead")]
        public async Task<IActionResult> GetAllItemDiscounts(CancellationToken cancellationToken)
        {
            var itemDiscounts = await _itemDiscountService.GetAllItemDiscountsAsync(cancellationToken);
            return Ok(itemDiscounts);
        }

        [HttpPost]
        [Authorize("ItemDiscountWrite")]
        public async Task<IActionResult> CreateItemDiscount([FromBody] ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken)
        {
            var createdItemDiscount = await _itemDiscountService.CreateItemDiscountAsync(itemDiscountDto, cancellationToken);
            return Ok(createdItemDiscount);
        }

        [HttpGet("{id}")]
        [Authorize("ItemDiscountRead")]
        public async Task<IActionResult> GetItemDiscountById(int id, CancellationToken cancellationToken)
        {
            var itemDiscount = await _itemDiscountService.GetItemDiscountByIdAsync(id, cancellationToken);
            return Ok(itemDiscount);
        }

        [HttpDelete("{id}")]
        [Authorize("ItemDiscountWrite")]
        public async Task<IActionResult> DeleteItemDiscountById(int id, CancellationToken cancellationToken)
        {
            await _itemDiscountService.DeleteItemDiscountAsync(id, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize("ItemDiscountWrite")]
        public async Task<IActionResult> UpdateItemDiscountById(int id, [FromBody] ItemDiscountRequest itemDiscountDto, CancellationToken cancellationToken)
        {
            var updatedItemDiscount = await _itemDiscountService.UpdateItemDiscountAsync(id, itemDiscountDto, cancellationToken);
            return Ok(updatedItemDiscount);
        }

        [HttpPut("{id}/link")]
        [Authorize("ItemDiscountWrite")]
        public async Task<IActionResult> LinkItemDiscountToItems(int id, [FromQuery] bool itemsAreProducts, [FromBody] int[] itemIdList, CancellationToken cancellationToken)
        {
            await _itemDiscountService.LinkItemDiscountToItemsAsync(id, itemsAreProducts, itemIdList, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}/unlink")]
        [Authorize("ItemDiscountWrite")]
        public async Task<IActionResult> UnlinkItemDiscountFromItems(int id, [FromQuery] bool itemsAreProducts, [FromBody] int[] itemIdList, CancellationToken cancellationToken)
        {
            await _itemDiscountService.UnlinkItemDiscountFromItemsAsync(id, itemsAreProducts, itemIdList, cancellationToken);

            return Ok();
        }

        //Leave timeStamp null if you want to get only the active items
        [HttpGet("item/{id}")]
        [Authorize("ItemDiscountRead")]
        public async Task<IActionResult> GetItemDiscountsLinkedToItemId(int id, [FromQuery] bool isProduct, [FromQuery] DateTime? timeStamp, CancellationToken cancellationToken)
        {
            var itemDiscounts = await _itemDiscountService.GetItemDiscountsLinkedToItemId(id, isProduct, timeStamp, cancellationToken);
            return Ok(itemDiscounts);
        }
    }
}
