using Xunit;
using FlowerREST;
using System.Collections.Generic;
using System.Linq;


namespace FlowerREST.Tests
{
    public class RepositoryFlowersTests
    {
        [Fact]
        public void Add_ShouldAddFlowerToList()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower flower = new Flower
            {
                Color = "Red"
            };

            // Act
            repo.Add(flower);

            IEnumerable<Flower> flowers = repo.GetAll();

            // Assert
            Xunit.Assert.Single(flowers);
        }

        [Fact]
        public void Add_ShouldAssignID()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower flower = new Flower
            {
                Color = "Blue"
            };

            // Act
            Flower result = repo.Add(flower);

            // Assert
            Xunit.Assert.True(result.ID > 0);
        }

        [Fact]
        public void GetByID_WhenFlowerExists_ShouldReturnFlower()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower addedFlower = repo.Add(
                new Flower
                {
                    Color = "Yellow"
                });

            // Act
            Flower? result =
                repo.GetByID(addedFlower.ID);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Yellow", result.Color);
        }

        [Fact]
        public void GetByID_WhenFlowerDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            // Act
            Flower? result =
                repo.GetByID(999);

            // Assert
            Xunit.Assert.Null(result);
        }

        [Fact]
        public void Delete_WhenFlowerExists_ShouldRemoveFlower()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            Flower addedFlower =
                repo.Add(new Flower
                {
                    Color = "Green"
                });

            // Act
            Flower? deletedFlower =
                repo.Delete(addedFlower.ID);

            IEnumerable<Flower> flowers =
                repo.GetAll();

            // Assert
            Xunit.Assert.NotNull(deletedFlower);
            Xunit.Assert.Empty(flowers);
        }

        [Fact]
        public void Delete_WhenFlowerDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            // Act
            Flower? result =
                repo.Delete(999);

            // Assert
            Xunit.Assert.Null(result);
        }

        [Fact]
        public void Update_WhenFlowerExists_ShouldUpdateColor()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            Flower addedFlower =
                repo.Add(new Flower
                {
                    Color = "Red"
                });

            Flower updatedFlower =
                new Flower
                {
                    Color = "Purple"
                };

            // Act
            Flower? result =
                repo.Update(
                    addedFlower.ID,
                    updatedFlower);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Purple", result.Color);
        }

        [Fact]
        public void Update_WhenFlowerDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            Flower updatedFlower =
                new Flower
                {
                    Color = "Purple"
                };

            // Act
            Flower? result =
                repo.Update(
                    999,
                    updatedFlower);

            // Assert
            Xunit.Assert.Null(result);
        }

        [Fact]
        public void GetAll_WhenRepositoryIsEmpty_ShouldReturnEmptyCollection()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            // Act
            IEnumerable<Flower> flowers =
                repo.GetAll();

            // Assert
            Xunit.Assert.Empty(flowers);
        }

        [Fact]
        public void GetAll_WhenFlowersExist_ShouldReturnAllFlowers()
        {
            // Arrange
            RepositoryFlowers repo =
                new RepositoryFlowers();

            repo.Add(new Flower { Color = "Red" });
            repo.Add(new Flower { Color = "Blue" });

            // Act
            IEnumerable<Flower> flowers =
                repo.GetAll();

            // Assert
            Xunit.Assert.Equal(2, flowers.Count());
        }
    }
}