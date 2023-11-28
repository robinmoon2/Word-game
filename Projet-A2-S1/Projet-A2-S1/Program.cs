using System.Globalization;
using System.Threading;
using ConsoleAppVisuals;


namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Core.ClearWindow();
            Core.SetTitle("Jeu des mots");
            Core.WriteTitle();
            Core.WriteBanner();
            bool EndMenu = true;
            while(EndMenu){
                var index= Core.ScrollingMenuSelector(" Menu Principal :", default, default, "Jouer", "Paramètre", "Quitter le jeu");
                switch(index.Item1){
                    case 0:
 
                            switch(index.Item2){
                            case 0:
                                int number = Method.TimedNumberInput(5);  // 5 seconds time limit
                                if (number == -1)
                                {
                                    Console.WriteLine("You didn't enter a number in time.");
                                }
                                else
                                {
                                    Console.WriteLine($"You entered: {number}");
                                }
                                EndMenu = false;
                            break;
                            
                            case 1:
                                var index2 =Core.ScrollingMenuSelector("Liste des paramètres :",default , default, "Changer couleur","Changer langue ","Revenir au menu principal");
                                switch(index2.Item2){
                                    case 0 : 
                                        var color = Core.ScrollingMenuSelector("Choisir une couleur :",default,default,"Rouge","Bleu","Vert","Jaune","Blanc","Noir");
                                        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Gray };
                                        ConsoleColor couleur = colors[color.Item2];
                                        Core.ChangeForeground(couleur);
                                        Core.WritePositionedString("Couleur choisie : ",Placement.Center,default,10,default);
                                    break;
                                }
                            break;

                            case 2 : 
                                EndMenu = false;
                                Core.ExitProgram();
                            break;                   
                        }
                    break;
                    case -1: 
                            Console.WriteLine("Vous avez pressé la touche echap, vous allez sortir du jeu, pressez entrée pour confirmer");
                            Console.ReadKey();
                            Core.ExitProgram();
                    break;
                    case -2: 
                            Console.WriteLine("Vous avez pressé la touche backspace, vous allez sortir du jeu, pressez entrée pour confirmer");
                            Console.ReadKey();
                            Core.ExitProgram();
                    break;
                }
                
                
            }
            
        }

        
    }
}
