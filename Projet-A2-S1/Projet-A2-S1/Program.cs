using System.Runtime.Serialization;

namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();

            // create the player
            Plateau plat= new Plateau();






            Method.CreatePlayer();

            var playerList = new List<Player>();
            PlayerList players = new PlayerList(playerList);
            
            Dictionnaire dico = new Dictionnaire();

            players.ReadYAML("data/config.yml");
            var index = Core.ScrollingMenuSelector("Voulez vous modifiez un nom ? ",default , default, "Oui","Non");
            if(index.Item2 == 0){
                Core.WritePositionedString("Quel pseudo modifier ? ",Placement.Left,default,10,default);
                string nameModif = Console.ReadLine() ?? "";
                Player? Modif = players.playerlist.FirstOrDefault(player => player.Name == nameModif);
                if (Modif != null)
                {
                    Modif.Name = "test";
                    players.WriteYAML("data/config.yml");
                }
                else{
                    Console.WriteLine("Le pseudo n'existe pas");
                }
                Core.ClearWindow();
            }
            Core.WritePositionedString(players.toString(),Placement.Right,default,10,default);
        }
    }
}

