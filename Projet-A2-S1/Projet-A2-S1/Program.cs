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
            Core.SetTitle("COUCOU");
            Core.WriteTitle();
            Core.WriteBanner();
            (int a , int b)= Core.ScrollingMenuSelector(" Menu Principal :", default, default, "Jouer", "Paramètre", "Quitter le jeu");
            switch(b){
                case 0:
                    int number = TimedNumberInput(5);  // 5 seconds time limit
                    if (number == -1)
                    {
                        Console.WriteLine("You didn't enter a number in time.");
                    }
                    else
                    {
                        Console.WriteLine($"You entered: {number}");
                    }
                break;
                case 1:
                    Console.WriteLine("liste des paramètres ");
                    (a,b)=Core.ScrollingMenuSelector("Liste des paramètres :",default , default, "Changer couleur","Changer langue ","Revenir au menu principal");
                break;
                case 2 : 
                    
                    Core.ExitProgram();
                break;                   

            }
        }



        public static int TimedNumberInput(int timeLimit)
        {
            Timer timer = null;
            Timer printTimer = null;
            bool timeUp = false;
            int remainingTime = timeLimit;

            timer = new Timer((state) =>
            {
                timeUp = true;
                timer.Dispose();
                printTimer.Dispose();
                Console.WriteLine("\nTime is up!");
            }, null, timeLimit * 1000, Timeout.Infinite);

            printTimer = new Timer((state) =>
            {
                remainingTime--;
                Core.WritePositionedString($"Remaining time: {remainingTime} seconds",Placement.Left,default,10,default);
            }, null, 1000, 1000);

            while (true)
            {
                Core.WritePositionedString("Enter a number: ",Placement.Center,default,10,default);
                string input = Console.ReadLine();

                if (timeUp)
                {
                    return -1;
                }


                if (int.TryParse(input, out int number))
                {
                    timer.Dispose();
                    printTimer.Dispose();
                    return number;
                }
            }
        }
    }
}
