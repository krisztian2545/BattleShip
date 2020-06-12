using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Data;

namespace ModelTests
{
    [TestClass]
    public class BotTests
    {

        static Player p1, p2;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            p1 = new Bot("Bot");
            p2 = new Player("Human", Bot.GenerateShips());

            Logger.Log("Bot's territory = " + Logger.TwoDimArrayToString<int>(p1.GetTerritory(true)));
            Logger.Log("Human's territory = " + Logger.TwoDimArrayToString<int>(p2.GetTerritory(true)));
        }

        [TestMethod]
        public void Shoot_WhenMisses_ShouldMarkWithTheNumber1()
        {
            // Arrange 
            int[,] table = p2.GetTerritory(true);
            Vector target = new Vector();
            for (int i = 0; i < 100; i++)
                if (table[i/10, i % 10] == 0)
                {
                    target = new Vector(i/10, i);
                    break;
                }

            // Act
            p1.AimAt(target);
            p1.Shoot(p2);

            // Assert
            Assert.AreEqual(1, p2.GetTerritory()[target.X, target.Y]);
        }

        [TestMethod]
        public void Shoot_WhenHit_ShouldMarkWithTheNumber3()
        {
            // Arrange 
            int[,] table = p2.GetTerritory(true);
            Vector target = new Vector();
            for (int i = 0; i < 100; i++)
                if (table[i / 10, i % 10] == 2)
                {
                    target = new Vector(i / 10, i);
                    break;
                }

            // Act
            p1.AimAt(target);
            p1.Shoot(p2);

            // Assert
            Assert.AreEqual(3, p2.GetTerritory()[target.X, target.Y]);
        }

        [TestMethod]
        public void GenerateShips_ShouldPlaceShipsCorrectly()
        {
            //Arrange & Act
            Ship[] ships = Bot.GenerateShips();
            foreach (Ship s in ships)
                Logger.Log(s.ToString());

            // Assert
            // see the printed table
        }

    }
}
