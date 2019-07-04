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
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository repository;
        private readonly IProductRepository productRepository;
        public ManufacturersController(IManufacturerRepository repository, IProductRepository productRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
        }
        //AddProductSupplier
        [HttpPut("{id}/products")]
        public IActionResult AddManufacturerProduct(int id, [FromBody] ProductManufacturer entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var manufacturer = repository.Retrieve(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            if (productRepository.Retrieve(entity.ProductID) == null)
            {
                return NotFound();
            }
            manufacturer.AddProduct(entity.ProductID);

            repository.Update(id, manufacturer);
            return Ok(manufacturer);

        }
        //Delete 
        [HttpPut("{id}/products/{productId}")]
        public IActionResult DeleteSupplier(int id, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var manufacturer = repository.Retrieve(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            if (productRepository.Retrieve(productId) == null)
            {
                return NotFound();
            }
            manufacturer.RemoveProduct(productId);
            repository.Update(id, manufacturer);
            return Ok();
        }


        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var manufacturer = repository.Retrieve(search, skip, count);
            return Ok(manufacturer);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var manufacturer = this.repository.Retrieve(id);
            return Ok(manufacturer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Manufacturer newManufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newManufacturer);

            return CreatedAtAction(nameof(this.Get), new { id = newManufacturer.ID }, newManufacturer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, manufacturer);

            return Ok(manufacturer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}
