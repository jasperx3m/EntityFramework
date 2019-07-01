using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.API.ViewModel;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;

namespace QuickReach.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository repository;
        private readonly IProductRepository productrepo;
        private readonly ECommerceDbContext context;
        public CategoriesController(ICategoryRepository repository, IProductRepository productrepo, ECommerceDbContext context)
        {
            this.repository = repository;
            this.productrepo = productrepo;
            this.context = context;
        }
        [HttpGet("{id}/products")]
        public IActionResult GetProductsByCategory(int id)
        {
            var connectionString =
            "Server=.;Database=QuickReachDb;Integrated Security=true;";
            var connection = new SqlConnection(connectionString);
            var sql = @"SELECT p.ID,
                         pc.ProductID, 
                         pc.CategoryID,
                         p.Name, 
                         p.Description,
                         p.Price,
                         p.ImageUrl
                    FROM Product p INNER JOIN ProductCategory pc ON p.ID = pc.ProductID
                    Where pc.CategoryID = @categoryId";
            var categories = connection
                .Query<SearchItemViewModel>(
                sql, new { categoryId = id })
                    .ToList();
            return Ok(categories);
        }
        [HttpGet]
        public IActionResult Get(string search ="", int skip = 0, int count = 10)
        {
            var categories = repository.Retrieve(search, skip, count);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = this.repository.Retrieve(id);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category newCategory)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Create(newCategory);

            return CreatedAtAction(nameof(this.Get), new {id=newCategory.ID}, newCategory);
        }
        //AddProductCategory
        [HttpPut("{id}/products")]
        public IActionResult AddCategoryProduct(int id, [FromBody] ProductCategory entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = repository.Retrieve(id);
            if (category == null)
            {
                return NotFound();
            }
            if (productrepo.Retrieve(entity.ProductID) == null)
            {
                return NotFound();
            }
            category.AddProduct(entity);

            repository.Update(id, category);
            return Ok(category);

        }
        //Delete ProductCategory
        [HttpPut("{id}/products/{productId}")]
        public IActionResult DeleteCategoryProduct(int id, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = repository.Retrieve(id);
            if (category == null)
            {
                return NotFound();
            }
            if (productrepo.Retrieve(productId) == null)
            {
                return NotFound();
            }
            category.RemoveProduct(productId);
            repository.Update(id, category);
            return Ok();
        }




        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.repository.Update(id, category);

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repository.Delete(id);

            return Ok();
        }
    }
}