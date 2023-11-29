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
            Method.CreatePlayer();
            Console.WriteLine("rajoutez un nombre au compteur ");
            int supp = Convert.ToInt32(Console.ReadLine());
            if(supp !=0){
                List<Player> playerlist = new List<Player>();
                PlayerList players = new PlayerList(playerlist);
                players.ReadYAML("data/config.yml");
                if(players.playerlist.Count >=2){
                    players.playerlist[1].Timer+=supp;
                    players.toString();
                    players.WriteYAML("data/config.yml");
                }
                else{
                    Console.WriteLine("LOLOOOLOLOO");
                }   
            }
        }

    }
}
