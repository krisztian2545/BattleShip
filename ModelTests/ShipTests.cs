using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;

namespace ModelTests
{
    [TestClass]
    public class ShipTests
    {

        [DataRow(3, 2, 1, true, 4, 2, 1, false)]
        [DataRow(5, 2, 1, true, 3, 3, 1, true)]
        [DataTestMethod]
        public void CollisionDetection_WhenShipsCollide_ReturnTrue(int s1Length, int s1PositionX, int s1PositionY, bool s1Horizontal, int s2Length, int s2PositionX, int s2PositionY, bool s2Horizontal)
        {
            // Arrange
            Ship s1 = new Ship(s1Length);
            s1.IsHorizontal = s1Horizontal;
            s1.Replace(new Vector(s1PositionX, s1PositionY));

            Ship s2 = new Ship(s2Length);
            s2.IsHorizontal = s2Horizontal;
            s2.Replace(new Vector(s2PositionX, s2PositionY));

            // Act & Assert
            Assert.IsTrue(Ship.CollisionDetection(s1, s2));

        }
    }
}
