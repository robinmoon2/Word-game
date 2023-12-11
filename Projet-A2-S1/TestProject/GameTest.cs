using System.Security;

namespace TestProject
{
    public class GameTest
    {
        
        [Test]
        [TestCase(100,2344)] // false
        [TestCase(0,0)] // true
        [TestCase(0,2344)] //false
        [TestCase(100,0)] // false
        public void TestEndGame(int timer1,int timer2){
            var game = new Game(true);
            game.Players = new PlayerList(listplayer(timer1,timer2));
            bool verif = game.EndGame();
            Assert.That(game.EndGame(), Is.EqualTo(verif));
        }


        public List<Player> listplayer(int timer1, int timer2){
            List<Player> ListOfPlayer = new List<Player>();
            Player player1 = new Player();
            player1.Name = "joueur1";
            player1.Timer = timer1;
            Player player2 = new Player();
            player2.Name = "joueur2";
            player2.Timer = timer2;
            ListOfPlayer.Add(player1);
            ListOfPlayer.Add(player2);
            return ListOfPlayer;
        }
    }

}