using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace QuickReach.ECommerce.Infra.Data.Tests
{
    public class SupplierRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateRecord()
        {

            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier
            {
                Name = "Blast Asia",
                Description = "Software Supplier",
                IsActive = true
            };


            //Act
            sut.Create(supplier);
            //Assert
            Assert.True(supplier.ID != 0);
            //Cleanup
            sut.Delete(supplier.ID);
            */
        }
        [Fact]
        public void Retrieve_WithValidID_ReturnsValidEntity()
        {
            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier
            {
                Name = "Blast Asia",
                Description = "Software Supplier",
                IsActive = true
            };
            sut.Create(supplier);
            //Act
            var actual = sut.Retrieve(supplier.ID);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Equals(supplier));

            //Cleanup
            sut.Delete(supplier.ID);
            */
        }
        [Fact]
        public void Delete_WithValidID_ShouldRemoveEntity()
        {
            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier
            {
                Name = "Blast Asia",
                Description = "Software Supplier",
                IsActive = true
            };
            sut.Create(supplier);
            Assert.True(supplier.ID != 0);
            //Act
            sut.Delete(supplier.ID);

            //Assert
            var actual = sut.Retrieve(supplier.ID);
            Assert.Null(actual);
            */
        }
        [Fact]
        public void Retrive_WithNonExistingID_ShouldReturnsNull()
        {
            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier
            {
                Name = "Blast Asia",
                Description = "Software Description",
                IsActive = true
            };
            sut.Create(supplier);
            Assert.True(supplier.ID != 0);
            sut.Delete(supplier.ID);
            //Act
            var actual = sut.Retrieve(supplier.ID);

            //Assert
            Assert.Null(actual);
            */
        }
        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            for (int i=0; i <20; i+=1)
            {
                sut.Create(new Supplier
                {
                    Name = string.Format("Supplier No. {0}", i),
                    Description = string.Format("Description No. {0}", i),
                    IsActive = true
                });
            }
            //Act
            var actual = sut.Retrieve(4, 3);
            //Assert
            Assert.True(actual.Count() == 3);
            //Cleanup
            var list = sut.Retrieve(0, Int32.MaxValue);
            foreach (var item in list)
            {
                sut.Delete(item.ID);
            }
            */
        }
        
        [Fact]
        public void Update_WithValidProperty_ShouldUpdateEntity()
        {
            /*
            //Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier
            {
                Name = "Blast Asia",
                Description = "Software Supplier",
                IsActive = true
            };
            sut.Create(supplier);
            var expected =sut.Retrieve(supplier.ID);

            //Act
            expected.Name = "Asus";
            expected.Description="Laptop Supplier";
            sut.Update(supplier.ID, expected);
            var actual = sut.Retrieve(supplier.ID);

            //Assert
            Assert.Equal(actual, expected);

            //Cleanup
            sut.Delete(supplier.ID);
            */
        }
    }
}
