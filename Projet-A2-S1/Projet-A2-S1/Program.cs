
using Microsoft.VisualBasic;

namespace Projet_A2_S1
{
    public class Program
    {
        static void Main()
        {

            
           
            Core.LoadingBar("Chargement du meilleur jeu du monde");
            Core.WriteContinuousString("Pour une meilleure expérience de jeu, mettez en pleine écran (F11 au cas ou) \nPressez n'importe quelle touche pour continuer ",5);
            Console.ReadKey();
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            Core.ChangeForeground(ConsoleColor.Red);
            Core.ApplyNegative(true);
            Core.WritePositionedString("Règles du jeu ", Placement.Center, default, 2, default);
            Core.ApplyNegative(false);
            Core.ChangeForeground(ConsoleColor.White);

            string explication = "Le but du jeu est de trouver des mots dans la grille de caractères donnée.\n" +
                "La première lettre de chaque mot doit être située dans la ligne du bas du plateau.\n" +
                "Pour trouver les mots, il faut aller de case en case en passant par la droite, la gauche, le haut et en diagonale. Il est impossible d'aller vers le bas.\n\n" +
                "Si vous avez trouvé un mot, il faut appuyer sur Entrée puis ÉCRIRE votre mot.\n" +
                "Maintenant, vous allez sélectionner le timer en secondes dont chaque joueur disposera, puis le nombre de joueurs et le plateau.\n" +
                "Pour le plateau, soit vous choisissez un exemple déjà conçu, soit une version aléatoire. Dans la version aléatoire, vous pouvez choisir la taille du plateau.\n\n" +
                "Pressez Entrée pour continuer";


            Core.WriteContinuousString(explication, 10);
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
            Core.ExitProgram();

        }
    }
}

