
using Microsoft.VisualBasic;

namespace Projet_A2_S1
{
    public class Program
    {
        static void Main()
        {

            
           
            Core.LoadingBar("Chargement du meilleur jeu du monde");
            Console.WriteLine("Pour une meilleure expérience de jeu, mettez en pleine écran (F11 au cas ou) Pressez entrée pour continuer ");
            Console.ReadKey();
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            Core.ApplyNegative(true);
            Core.ChangeForeground(ConsoleColor.Red);
            Core.WritePositionedString("Règles du jeu ", Placement.Center, default, 2, default);
            Core.ApplyNegative(false);
            Core.ChangeForeground(ConsoleColor.White);
            string explication = " \n\n" +
                "Le but du jeu est de trouver des mots dans la gille de charactère donnée\n" +
                "La première lettre de chaque mot doit être situé dans la ligne du bas du plateau \n" +
                "Pour trouver ls mots il faut aller de case en case en passant par la droite, gauche, haut et diagonale. Il est impossible d'aller en bas\n\n" +
                "Si vous avez trouvez un mot il faut presser entrée puis ECRIRE votre mot\n" +
                "Maintenant vous allez sélectionner le timer en seconde dont chaque joueur disposera, puis le nombre de joueur et le plateau \n"+
                "Pour le plateau soit vous choisissez un exemple déjà conçu ou bien une version aléatoire, dans la version aléatoire vous pouvez choisir la taille du plateau\n\n"+
                "Pressez Entree pour continuer";
            Core.WriteParagraph(false,5,explication);
            Console.ReadKey();
            Core.ClearWindow();
            Method.CreatePlayer(); // create the players
            var game = new Game();
            Core.ClearWindow();
            while(!game.EndGame()){
                game.Turn();
            }
            game.Results();
            // create the game

        }
    }
}

