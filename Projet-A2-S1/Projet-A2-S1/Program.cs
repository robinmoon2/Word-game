
namespace Projet_A2_S1
{
    public class Program
    {
        static void Main()
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            Method.CreatePlayer(); // create the players
            var game = new Game();
            Core.ClearWindow();
            while(!game.EndGame()){
                game.Turn();
            }
            
            // create the game

        }
    }
}

