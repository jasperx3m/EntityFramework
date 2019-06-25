using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var expected = new Supplier
            {
                Name = "Adidas",
                Description = "Shoe Supplier",
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var sut = new SupplierRepository(context);

                //Act
                sut.Create(expected);
            }
            using (var context = new ECommerceDbContext(options))
            {
                var actual = context.Suppliers.Find(expected.ID);
                //Assert
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.IsActive, actual.IsActive);
            }
            #region oldcreateversion
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
            #endregion
        }
        [Fact]
        public void Retrieve_WithValidID_ReturnsValidEntity()
        {
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var expected = new Supplier
            {
                Name = "Adidas",
                Description = "Shoe Supplier",
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Suppliers.Add(expected);
                context.SaveChanges();
            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new SupplierRepository(context);

                //Act
                var actual = sut.Retrieve(expected.ID);
                //Assert
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Description, actual.Description);
                Assert.Equal(expected.IsActive, actual.IsActive);
            }
            #region oldretrieveversion
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
            #endregion
        }
        [Fact]
        public void Delete_WithValidID_ShouldRemoveEntity()
        {
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var expected = new Supplier
            {
                Name = "Adidas",
                Description = "Shoe Supplier",
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Suppliers.Add(expected);
                context.SaveChanges();
            }
            using (var context = new ECommerceDbContext(options))
            {

                var sut = new SupplierRepository(context);
                //Act
                sut.Delete(expected.ID);

                //Assert
                var actual = context.Suppliers.Find(expected.ID);
                Assert.Null(actual);
            }

            #region oldretrievenullversion
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
            #endregion
        }
        [Fact]
        public void Retrive_WithNonExistingID_ShouldReturnsNull()
        {
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var expected = new Supplier
            {
                Name = "Adidas",
                Description = "Shoe Supplier",
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Suppliers.Add(expected);
                context.SaveChanges();
            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new SupplierRepository(context);
                sut.Delete(expected.ID);
                //Act
                var actual = sut.Retrieve(expected.ID);
                //Assert
                Assert.Null(actual);
            }
            #region retrievenullversion
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
            #endregion
        }
        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;


            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();


                for (int i = 0; i < 20; i += 1)
                {
                    context.Suppliers.Add(new Supplier
                    {
                        Name = string.Format("Supplier No. {0}", i),
                        Description = string.Format("Description No. {0}", i),
                        IsActive = true
                    });
                    context.SaveChanges();
                }
            }
            using (var context= new ECommerceDbContext(options))
            { 
                var sut = new SupplierRepository(context);
                //Act
                var actual = sut.Retrieve(3, 5);
                //Assert
                Assert.True(actual.Count() == 5);
            }
            #region oldretrievepageversion
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
            #endregion
        }
        
        [Fact]
        public void Update_WithValidProperty_ShouldUpdateEntity()
        {
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;

            var expected = new Supplier
            {
                Name = "Adidas",
                Description = "Shoe Supplier",
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Suppliers.Add(expected);
                context.SaveChanges();
            }
            expected.Name = "Nike";
            expected.Description = "New Shoe Supplier";
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new SupplierRepository(context);

                //Act 
                sut.Update(expected.ID, expected);

                //Assert
                var actual = context.Suppliers.Find(expected.ID);
                Assert.Equal(expected, actual);
            }
            #region oldupdateversion
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
            #endregion
        }
    }
}
