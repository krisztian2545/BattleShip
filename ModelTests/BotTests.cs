using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Data;

namespace ModelTests
{
    [TestClass]
    public class BotTests
    {

        [TestMethod]
        public void GenerateShips_ShouldPlaceShipsCorrectly()
        {
            //Arrange & Act
            Ship[] ships = Bot.GenerateShips();
            foreach (Ship s in ships)
                Logger.Log(s.ToString());
        }

    }
}
