using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace QuickReach.ECommerce.Infra.Data.Tests
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateRecord()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            var product = new Product
            {
                Name="Nike",
                Description="Airmax",
                Price=3000,
                Category=fillerCategory,
                ImageUrl="picture"
            };
            //Act
            sut.Create(product);
            Assert.True(product.ID != 0);
            var actual = sut.Retrieve(product.ID);

            //Assert
            Assert.NotNull(actual);

            //Cleanup
            sut.Delete(actual.ID);
            cat.Delete(fillerCategory.ID);
        }

        [Fact]
        public void Retrieve_WithValidID_ReturnsValidEntity()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            var product = new Product
            {
                Name = "Nike",
                Description = "Airmax",
                Price = 3000,
                Category = fillerCategory,
                ImageUrl = "picture"
            };

            sut.Create(product);
            Assert.True(product.ID != 0);

            //Act
            var actual = sut.Retrieve(product.ID);
            //Assert
            Assert.NotNull(actual);
            //Cleanup
            sut.Delete(actual.ID);
            cat.Delete(category.ID);
            
        }
        
        [Fact]
        public void Retrieve_WithNonExistingID_ReturnsNull()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            var product = new Product
            {
                Name = "Nike",
                Description = "Airmax",
                Price = 3000,
                Category = fillerCategory,
                ImageUrl = "picture"
            };

            sut.Create(product);
            Assert.True(product.ID != 0);
            var actual = sut.Retrieve(product.ID);
            sut.Delete(actual.ID);


            //Act
            actual = sut.Retrieve(actual.ID);
            //Assert
            Assert.Null(actual);
            //Cleanup
            cat.Delete(category.ID);

            
        }
        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            for (var i = 1; i < 20; i += 1)
            {
                sut.Create(new Product
                {
                    Name = string.Format("Category {0}", i),
                    Description = string.Format("Description {0}", i),
                    Price = 3000 + i,
                    Category = fillerCategory,
                    ImageUrl = string.Format("Image {0}", i)
                });
            }


            //Act
            var list = sut.Retrieve(5, 5);
            //Assert
            Assert.True(list.Count() == 5);
            //Cleanup
            var prodlist = sut.Retrieve(0, Int32.MaxValue);
            foreach (var item in prodlist)
            {
                sut.Delete(item.ID);
            }
            var catlist = cat.Retrieve(0, Int32.MaxValue);
            foreach (var item in catlist)
            {
                cat.Delete(item.ID);
            }
        }
        [Fact]

        public void Delete_WithValidID_ShouldDeleteRecord()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            var product = new Product
            {
                Name = "Nike",
                Description = "Airmax",
                Price = 3000,
                Category = fillerCategory,
                ImageUrl = "picture"
            };

            sut.Create(product);
            var actual = sut.Retrieve(product.ID);
            Assert.NotNull(actual);

            //Act
            sut.Delete(product.ID);
            //Assert
            actual = sut.Retrieve(product.ID);
            Assert.Null(actual);
            //Cleanup
            cat.Delete(category.ID);
            
        }
        
        [Fact]
        public void Update_WithValidProperty_ShouldUpdateRecord()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var cat = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            cat.Create(category);
            var fillerCategory = cat.Retrieve(category.ID);

            var sut = new ProductRepository(context);
            var product = new Product
            {
                Name = "Nike",
                Description = "Airmax",
                Price = 3000,
                Category = fillerCategory,
                ImageUrl = "picture"
            };

            sut.Create(product);
            var expected = sut.Retrieve(product.ID);
            Assert.NotNull(expected);

            //Act
            expected.Name = "Adidas";
            expected.Description = "Tubular";
            sut.Update(product.ID, expected);

            //Assert
            var actual = sut.Retrieve(product.ID);
            Assert.True(actual.Equals(expected));

            //Cleanup
            sut.Delete(expected.ID);
            cat.Delete(category.ID);
        }

    }
}
