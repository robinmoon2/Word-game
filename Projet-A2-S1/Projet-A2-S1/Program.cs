using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Projet_A2_S1
{
    public class Program
    {
        static void Main()
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();

            // create the player
            var plat = new GameBoard();
            plat.Read();
            Console.WriteLine(plat);
            Console.WriteLine("Rentrez un mot : ");
            string mot = Console.ReadLine() ?? "";
            Dictionary<(int,int), char> dicotest = new Dictionary<(int,int), char>();
            for (int y = 0; y < plat.Board.GetLength(1) ; y++)
            {
                if(plat.Board[plat.Board.GetLength(0) - 1, y] == mot[0]) // si la lettre est la même que la première lettre du mot (on commence par la dernière ligne du plateau car on cherche le mot à l'envers)
                {
                    dicotest = plat.GetWord(plat.Board.GetLength(0) - 1, y, mot); // on lance la recherche du mot
                }
            }
            plat.SaveAndWrite();
            foreach(var item in dicotest) // on affiche le dictionnaire les coordonnées et les lettres)
            {
                plat.Board[item.Key.Item1, item.Key.Item2] = ' ';
            }
            plat.Maj_Plateau();
            plat.SaveAndWrite();
            Console.WriteLine(plat);

            
        }
    }
}

