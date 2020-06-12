using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Data;


namespace ModelTests
{
    [TestClass]
    public class GameTests
    {
        Game g;
        Player p1, p2;

        [TestInitialize]
        public void BeforeEach()
        {
            p1 = new Player("Naruto", Bot.GenerateShips());
            p2 = new Bot("Sasuke");
            g = new Game(p1, p2);
            g.InitGame();
        }

        [TestMethod]
        public void Next_ShouldNotBreak()
        {
            // Arrange
            g.GetCurrentPlayer().AimAt(new Vector(5, 6));

            Logger.Log("---------------------- ROUND 1 ------------------------");
            Logger.Log("Naruto's territory = " + Logger.TwoDimArrayToString<int>(p1.GetTerritory(true)));
            Logger.Log("Sasuke's territory = " + Logger.TwoDimArrayToString<int>(p2.GetTerritory(true)));

            // Act
            g.Next();

            Logger.Log("---------------------- TURN 1 ------------------------");
            Logger.Log("Naruto's territory = " + Logger.TwoDimArrayToString<int>(p1.GetTerritory(true)));
            Logger.Log("Sasuke's territory = " + Logger.TwoDimArrayToString<int>(p2.GetTerritory(true)));
            g.Next();

            Logger.Log("---------------------- TURN 2 ------------------------");
            Logger.Log("Naruto's territory = " + Logger.TwoDimArrayToString<int>(p1.GetTerritory(true)));
            Logger.Log("Sasuke's territory = " + Logger.TwoDimArrayToString<int>(p2.GetTerritory(true)));

        }
    }
}
