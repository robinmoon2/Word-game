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
            List<Player> player = new List<Player>();
            PlayerList players = new PlayerList(player);
            players.ReadYAML("data/config.yml");
            Method.CreatePlayer();
            Console.WriteLine("program");
            Player p = new Player{Name = "test", Timer = 0, Score = 0, WordList = new List<string>()};
            Console.WriteLine(p.Word_Value("TEST"));
            Console.WriteLine(p.Word_Value("ABCD"));

        }

    }
}
