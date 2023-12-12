
using Microsoft.VisualBasic;
using System;

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

            string explication = "Le jeu a pour but de trouver des mots qui sont présents dans un plateau de caractères.\r\n\r\nPour former un mot, la première lettre du mot doit être dans la dernière ligne du plateau (celle du bas) et ensuite on peut aller de caractère en caractère avec celui d'à côté, d'en haut, sur les diagonales hautes et basses. \r\n\r\nPour rentrer un mot :\r\nVous avez chacun un timer global que vous allez définir, vous allez devoir chercher un mot pendant que votre timer baisse, une fois arrivé à 0 vous ne pouvez plus rentrer de mot. Pour rentrer un mot (quand vous en avez trouvés un) Vous pressez la touche entrée et vous l'écrivez puis à nouveau entrée. \r\n\r\nAttention, il faut que le mot existe dans la langue française, qu'il ne soit pas dans votre liste et qu'il soit sur le plateau. \r\n\r\nBonne partie à vous  !";


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

