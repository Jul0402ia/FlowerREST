using FlowerREST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FlowerREST.Tests
{
    [TestClass]
    public class RepositoryFlowersTests
    {
        [TestMethod]
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

            var flowers = repo.GetAll();

            // Assert
            Assert.AreEqual(1, flowers.Count());
        }

        [TestMethod]
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
            Assert.IsTrue(result.ID > 0);
        }

        [TestMethod]
        public void GetByID_WhenFlowerExists_ShouldReturnFlower()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower addedFlower = repo.Add(new Flower
            {
                Color = "Yellow"
            });

            // Act
            Flower? result = repo.GetByID(addedFlower.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Yellow", result.Color);
        }

        [TestMethod]
        public void GetByID_WhenFlowerDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            // Act
            Flower? result = repo.GetByID(999);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete_WhenFlowerExists_ShouldRemoveFlower()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower addedFlower = repo.Add(new Flower
            {
                Color = "Green"
            });

            // Act
            Flower? deletedFlower = repo.Delete(addedFlower.ID);

            var flowers = repo.GetAll();

            // Assert
            Assert.IsNotNull(deletedFlower);
            Assert.AreEqual(0, flowers.Count());
        }

        [TestMethod]
        public void Update_WhenFlowerExists_ShouldUpdateColor()
        {
            // Arrange
            RepositoryFlowers repo = new RepositoryFlowers();

            Flower addedFlower = repo.Add(new Flower
            {
                Color = "Red"
            });

            Flower updatedFlower = new Flower
            {
                Color = "Purple"
            };

            // Act
            Flower? result = repo.Update(addedFlower.ID, updatedFlower);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Purple", result.Color);
        }
    }
}