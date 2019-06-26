using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System;
using Xunit;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using QuickReach.ECommerce.Infra.Data.Repository;

namespace QuickReach.ECommerce.Infra.Data.Tests
{
    public class CategoryRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                   .UseInMemoryDatabase($"CategoryForTesting{Guid.NewGuid()}")
                   .Options;
            var expected = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

           

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Create(expected);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Description, actual.Description);

            }
            #region oldcreateversion
            /*
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
           */ 
            #endregion
        }


        [Fact]
        public void Retrieve_WithValidEntityID_ReturnsAValidEntity()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                   .UseInMemoryDatabase($"CategoryForTesting{Guid.NewGuid()}")
                   .Options;
            var expected = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

            using (var context = new ECommerceDbContext(options))
            {
                context.Categories.Add(expected);
                context.SaveChanges();

            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Retrieve(expected.ID);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Description, actual.Description);

            }
            #region oldretrieveversion
            /*
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
                */ 
            #endregion
        }
        [Fact]
        public void Retrieve_WithNonExistingEntityID_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                   .UseInMemoryDatabase($"CategoryForTesting{Guid.NewGuid()}")
                   .Options;
            var expected = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

            using (var context = new ECommerceDbContext(options))
            {
                context.Categories.Add(expected);
                context.SaveChanges();
                context.Categories.Remove(expected);
                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Retrieve(expected.ID);

                // Assert
                Assert.Null(actual);
               

            }
            #region oldretrievenullversion
            /*
                //Arrange
                var context = new ECommerceDbContext();
                var sut = new CategoryRepository(context);

                //Act
                var actual = sut.Retrieve(-1);
                //Assert
                Assert.Null(actual);
                */ 
            #endregion
        }

         

        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                   .UseInMemoryDatabase($"CategoryForTesting{Guid.NewGuid()}")
                   .Options;
           

            using (var context = new ECommerceDbContext(options))
            {
                for(int i=1;i<20;i+=1)
                {
                    context.Categories.Add(new Category
                    {
                        Name = string.Format("Category {0}", i),
                        Description = string.Format("Description {0}", i)
                    });
                }
                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Retrieve(4,5);

                // Assert
                Assert.True(actual.Count() == 5);
                

            }
            #region oldretrievepageversion
            /*
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
                */ 
            #endregion
        }
        
        [Fact]
        public void Delete_WithValidID_ShouldRemoveRecord()
        {
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseSqlite(connection)
                    .Options;
            var expected = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Categories.Add(expected);
                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                sut.Delete(expected.ID);

                // Assert
                var actual = context.Categories.Find(expected.ID);
                Assert.Null(actual);


            }
            #region olddeleteversion
            /*
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
               */ 
            #endregion
        }
        [Fact]
        public void Delete_WithExistingProducts_ShouldReturnException()
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
                Name = "Shoes",
                Description = "Shoes Department"
            };
            var product = new Product
            {
                Name = "Adidas Tubular",
                Description = "Adidas Shoe",
                ImageUrl="adidas.com",
                Category=category,
                Price=4000,
                IsActive = true
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Categories.Add(category);
                context.Products.Add(product);
                context.SaveChanges();
            }
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                
                //Act // Assert
                Assert.Throws<Exception>(() => sut.Delete(category.ID));


            }
        }

        [Fact]
        public void Update_WithValidProperty_ShouldUpdateEntity()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                    .UseInMemoryDatabase($"CategoryForTesting{Guid.NewGuid()}")
                    .Options;


            var expected = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };
            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Categories.Add(expected);
                context.SaveChanges();
            }
            expected.Name = "Slippers";
            expected.Description = "Slippers Department";
            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                //Act 
                sut.Update(expected.ID, expected);

                //Assert
                var actual = context.Categories.Find(expected.ID);
                Assert.Equal(expected, actual);
            }
            #region newupdateversion
            /*
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
                */ 
            #endregion
        }
    }
}
