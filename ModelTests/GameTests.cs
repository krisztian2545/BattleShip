using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Data;


namespace ModelTests
{
    [TestClass]
    public class GameTests
    {
        Game g;

        [TestInitialize]
        public void BeforeEach()
        {
            Player p1 = new Player("Naruto", Ship.CreateCrew(2, 3, 3, 4, 5));
            Player p2 = new Bot("Sasuke");
            g = new Game(p1, p2);
            g.InitGame();
        }

        [TestMethod]
        public void Next_ShouldNotBreak()
        {
            // Arrange
            g.GetCurrentPlayer().AimAt( new Vector(5, 6) );

            // Act
            g.Next();
            g.Next();

            int[] ss = new int[5];
            Logger.Log("ss No 3 = " + ss[2]);
        }
    }
}
