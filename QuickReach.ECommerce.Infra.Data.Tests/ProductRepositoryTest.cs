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
            var actual = sut.Retrieve(product.ID);

            //Assert
            Assert.NotNull(actual.ID);

            //Cleanup
            var list = sut.Retrieve(0, Int32.MaxValue);
            foreach (var item in list)
            {
                sut.Delete(item.ID);
            }
            var catlist = cat.Retrieve(0, Int32.MaxValue);
            foreach (var item in catlist)
            {
                cat.Delete(item.ID);
            }
        }
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
            for (var i = 1; i < 20; i += 1)
            {
                sut.Create(new Product
                {
                    Name = string.Format("Category {0}", i),
                    Description = string.Format("Description {0}", i),
                    Price= 3000+i,
                    Category=fillerCategory,
                    ImageUrl=string.Format("Image {0}",i)
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
        public void Retrieve_WithNonExistingID_ReturnsNull()
        {
            //Arrange
            //Act
            //Assert
            //Cleanup
        }
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            //Arrange
            //Act
            //Assert
            //Cleanup
        }
        public void Delete_WithValidID_ShouldDeleteRecord()
        {
            //Arrange
            //Act
            //Assert
            //Cleanup
        }

    }
}
