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
                                Jeu.beginning();
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



        /// <summary>
        /// Fonction permettant de laisser un temps donné à l'utilisateur pour faire une action
        /// </summary>
        /// <param name="timeLimit"> variable qui permet de stocker la limite de temps donnée</param>
        /// <returns></returns>

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
