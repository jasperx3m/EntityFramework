using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System;
using Xunit;
using System.Collections;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.Tests
{
    public class CategoryRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

            //Act
            sut.Create(category);

            //Assert
            Assert.True(category.ID != 0);

            var entity = sut.Retrieve(category.ID);
            Assert.NotNull(entity);

            //Cleanup
            sut.Delete(category.ID);

        }
        [Fact]
        public void Retrieve_WithValidEntityID_ReturnsAValidEntity()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };
            var sut = new CategoryRepository(context);
            sut.Create(category);

            //Act
            var actual = sut.Retrieve(category.ID);
            //Assert
            Assert.NotNull(actual);
            //Cleanup
            sut.Delete(actual.ID);
        }
        [Fact]
        public void Retrieve_WithNonExistingEntityID_ReturnsNull()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);

            //Act
            var actual = sut.Retrieve(-1);
            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            for (var i = 1; i < 20; i += 1)
            {
                sut.Create(new Category
                {
                    Name = string.Format("Category {0}", i),
                    Description = string.Format("Description {0}", i)
                });
            }

            //Act
            var list = sut.Retrieve(5, 5);

            //Assert
            Assert.True(list.Count() == 5);

            //Cleanup
            list = sut.Retrieve(0, Int32.MaxValue);
            list.All(c => { sut.Delete(c.ID); return true; });
        }
        
        [Fact]
        public void Delete_WithValidID_ShouldRemoveRecord()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department",
                IsActive = true
            };

            sut.Create(category);
            var actual=sut.Retrieve(category.ID);
            Assert.NotNull(actual);
            //Act
            sut.Delete(category.ID);
            //Assert
            actual = sut.Retrieve(category.ID);
            Assert.Null(actual);
        }

        [Fact]
        public void Update_WithValidProperty_ShouldUpdateEntity()
        {
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var oldCategory = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department",
                IsActive = true
            };
            sut.Create(oldCategory);
            

            var retrieve = sut.Retrieve(oldCategory.ID);
            retrieve.Name = "Pants";
            retrieve.Description = "Pants Department";

            //Act
            sut.Update(retrieve.ID, retrieve);

            //Assert
            var expected = sut.Retrieve(retrieve.ID);
            Assert.True(retrieve.Equals(expected));

            //Cleanup
            sut.Delete(expected.ID);
        }
    }
}
