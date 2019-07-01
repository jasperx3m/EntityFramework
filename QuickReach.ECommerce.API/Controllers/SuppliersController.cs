using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;

namespace QuickReach.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository repository;
        private readonly IProductRepository productRepository;
        private readonly ECommerceDbContext context;
        public SuppliersController(ISupplierRepository repository, IProductRepository productRepository, ECommerceDbContext context)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.context = context;
        }
        //AddProductSupplier
        [HttpPut("{id}/products")]
        public IActionResult AddSupplierProduct(int id, [FromBody] ProductSupplier entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var supplier = repository.Retrieve(id);
            if (supplier == null)
            {
                return NotFound();
            }
            if (productRepository.Retrieve(entity.ProductID) == null)
            {
                return NotFound();
            }
            supplier.AddProduct(entity.ProductID);

            repository.Update(id, supplier);
            return Ok(supplier);

        }
        //Delete ProductSupplier
        [HttpPut("{id}/products/{productId}")]
        public IActionResult DeleteSupplier(int id, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var supplier = repository.Retrieve(id);
            if (supplier == null)
            {
                return NotFound();
            }
            if (productRepository.Retrieve(productId) == null)
            {
                return NotFound();
            }
            supplier.RemoveProduct(productId);
            repository.Update(id, supplier);
            return Ok();
        }


        [HttpGet]
        public IActionResult Get(string search = "", int skip = 0, int count = 10)
        {
            var supplier = repository.Retrieve(search, skip, count);
            return Ok(supplier);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supplier = this.repository.Retrieve(id);
            return Ok(supplier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Supplier newSupplier)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newSupplier);

            return CreatedAtAction(nameof(this.Get), new { id = newSupplier.ID }, newSupplier);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, supplier);

            return Ok(supplier);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}