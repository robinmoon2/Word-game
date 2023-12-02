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
            Dictionnaire dico = new Dictionnaire();
            Console.WriteLine("coucou");
            Console.WriteLine(dico.toString());
            Console.WriteLine(dico.FindWord("BUCHEUR"));
        }
    }
}

