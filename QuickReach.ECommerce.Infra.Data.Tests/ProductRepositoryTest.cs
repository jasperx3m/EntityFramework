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
    public class ProductRepositoryTest
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

            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            using (var context = new ECommerceDbContext(options))
            {
                
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                context.SaveChanges();
                
            }
            using (var context = new ECommerceDbContext(options))
            {
                var expected = new Product
                {
                    Name = "Tubular",
                    Description = "Adidas Shoe",
                    CategoryID = category.ID,
                    ImageUrl = "image.com",
                    Price = 3000
                };
                var sut = new ProductRepository(context);

                //Act
                sut.Create(expected);
                var actual = context.Products.Find(expected.ID);
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
               */ 
            #endregion
        }
        [Fact]
        public void Create_WithNonExistingCategory_ReturnsException()
        {
            //Assert
            //Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;
            
            var product = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                CategoryID = 12,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context= new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);
                //Act //Assert
                Assert.Throws<Exception>(()=> sut.Create(product));
            }
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
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            var expected = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                Category = category,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                context.Products.Add(expected);
                context.SaveChanges();
                
            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);

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
                */ 
            #endregion
        }
        
        [Fact]
        public void Retrieve_WithNonExistingID_ReturnsNull()
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
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            var expected = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                Category = category,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                context.Products.Add(expected);
                context.Products.Remove(expected);
                context.SaveChanges();

            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);

                //Act
                var actual = sut.Retrieve(expected.ID);

                //Assert
                Assert.Null(actual);
                
            }
            #region oldretrievenullversion
            /*
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
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            var expected = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                Category = category,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                for (int i=1; i < 20; i += 1)
                {
                    context.Products.Add(new Product
                    {
                        Name = string.Format("Category {0}", i),
                        Description = string.Format("Description {0}", i),
                        Price = 3000 + i,
                        Category = category,
                        ImageUrl = string.Format("Image{0}.com", i)
                    });
                }
                
                context.SaveChanges();

            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);

                //Act
                var actual = sut.Retrieve(4,5);

                //Assert
                Assert.True(actual.Count()== 5);
            }

            #region oldretrievepageversion
            /*
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
                */ 
            #endregion
        }
        [Fact]

        public void Delete_WithValidID_ShouldDeleteRecord()
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
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            var expected = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                Category = category,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                context.Products.Add(expected);
                context.SaveChanges();

            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);

                //Act
                sut.Delete(expected.ID);

                //Assert
                var actual =context.Products.Find(expected.ID);
                Assert.Null(actual);
                
            }
            #region olddeleteversion
            /*
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
                */ 
            #endregion
        }
        
        [Fact]
        public void Update_WithValidProperty_ShouldUpdateRecord()
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
            var category = new Category
            {
                Name = "Shoe",
                Description = "Shoe Department"
            };
            var expected = new Product
            {
                Name = "Tubular",
                Description = "Adidas Shoe",
                Category = category,
                ImageUrl = "image.com",
                Price = 3000
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(category);
                context.Products.Add(expected);
                context.SaveChanges();

            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);
                expected.Name = "Yeezy Boost";
                expected.Description = "Adidas top 4";

                //Act
                var actual = sut.Update(expected.ID,expected);

                //Assert
                Assert.Equal(actual,expected);
            }
            #region oldupdateversion
            /*
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
                */ 
            #endregion
        }

    }
}
