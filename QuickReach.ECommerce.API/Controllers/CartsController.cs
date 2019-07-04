using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.API.ViewModel;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;

namespace QuickReach.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository repository;
        private readonly IProductRepository productrepo;

        public CartsController(ICartRepository repository, IProductRepository productrepo)
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
        public IActionResult Post([FromBody] Cart newCart)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newCart);

            return CreatedAtAction(nameof(this.Get), new { id = newCart.ID }, newCart);
        }
        //AddCart
        [HttpPut("{id}/items")]
        public IActionResult AddCartItem(int id, [FromBody] CartItem item)
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
            if (productrepo.Retrieve(item.ProductId) == null)
            {
                return NotFound();
            }
            cart.AddItem(item);

            repository.Update(id, cart);
            return Ok(cart);

        }
        //Delete 

        [HttpPut("{id}/items/{productId}")]
        public IActionResult DeleteCartItem(int id, int productId)
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
            if (productrepo.Retrieve(productId) == null)
            {
                return NotFound();
            }
            cart.RemoveItem(productId);
            repository.Update(id, cart);
            return Ok();
        }




        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, cart);

            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}
    