using Discount.API.Entities;
using Discount.API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}")]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
        public async Task<ActionResult<Coupon>> GetByProductName(string productName)
        {
            return Ok(await _discountRepository.GetByProductName(productName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
        {
            await _discountRepository.Create(coupon);
            return CreatedAtRoute("GetByProductName", new { productName = coupon.ProductName });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Coupon>> Update([FromBody] Coupon coupon)
        {
            return Ok(await _discountRepository.Update(coupon));
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete(string productName)
        {
            return Ok(await _discountRepository.Delete(productName));
        }
    }
}
