using System.Globalization;
using System.Threading;
using System.IO;
using ConsoleAppVisuals;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            List<Player> playerlist = new List<Player>();
            PlayerList players = new PlayerList(playerlist);
            players.ReadYAML("data/config.yml");
            Method.CreatePlayer();

            // * Test

            Console.WriteLine("program");
            players.ReadYAML("data/config.yml");
            string mot1 = "test";
            foreach(Player p in players.playerlist)
            {
                if(!p.Contient(mot1)){
                    p.Add_Mot(mot1);
                    p.Add_Score(p.Word_Value(mot1));
                    Console.WriteLine(p.toString());
                }
                else{
                    Console.WriteLine("Mot déjà dans votre liste ! ");
                }
            }
        }
    }
}

