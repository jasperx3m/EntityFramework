using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;

namespace QuickReach.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repository;
        private readonly IProductRepository productrepo;

        public OrderController(IOrderRepository repository, IProductRepository productrepo)
        {
            this.repository = repository;
            this.productrepo = productrepo;
        }

        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var carts = repository.Retrieve(search, skip, count);
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cart = this.repository.Retrieve(id);
            return Ok(cart);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order newOrder)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newOrder);

            return CreatedAtAction(nameof(this.Get), new { id = newOrder.ID }, newOrder);
        }
        //AddCart
        [HttpPut("{id}/products")]
        public IActionResult AddCartItem(int id, [FromBody] OrderItem entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var cart = repository.Retrieve(id);
            if (cart == null)
            {
                return NotFound();
            }
            if (productrepo.Retrieve(entity.ProductId) == null)
            {
                return NotFound();
            }
            cart.AddItem(entity);

            repository.Update(id, cart);
            return Ok(cart);

        }
        //Delete ProductCategory
        [HttpPut("{id}/products/{productId}")]
        public IActionResult DeleteCategoryProduct(int id, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var order = repository.Retrieve(id);
            if (order == null)
            {
                return NotFound();
            }
            if (productrepo.Retrieve(productId) == null)
            {
                return NotFound();
            }
            order.RemoveItem(productId);
            repository.Update(id, order);
            return Ok();
        }




        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, order);

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}
