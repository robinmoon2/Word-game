using YamlDotNet.Core;

namespace Projet_A2_S1
{
    public class Method
    {   
        /// <summary>
        /// This function create the main menu of the game
        /// </summary>
        static public void main_menu(){
            Core.ClearWindow();
            Core.SetTitle("Mots Glissants");
            Core.WriteTitle();
            Core.WriteBanner();
            bool EndMenu = true;
            while(EndMenu){
                var index= Core.ScrollingMenuSelector(" Menu Principal :", default, default, "Jouer", "Paramètre", "Quitter le jeu");
                switch(index.Item1){

                    case 0:
                        switch(index.Item2){

                            case 0:
                                EndMenu=false;
                                
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
        public static (int, string?) TimedInput(int timeLimit, string message = "Pressez la touche ENTREE pour ensuite rentrer un mot :  ")
        {
            Timer? timer = null;
            Timer? printTimer = null;
            bool timeUp = false;
            int remainingTime = timeLimit;

            timer = new Timer((state) =>
            {
                timeUp = true;
                timer?.Dispose();
                printTimer?.Dispose();
                Core.WritePositionedString("Time is up",Placement.Center,default,20,default);
            }, null, timeLimit * 1000, Timeout.Infinite);

            printTimer = new Timer((state) =>
            {
                remainingTime--;
                if(remainingTime < 10){
                    Core.ClearLine(20);
                }
                Core.WritePositionedString($"Remaining time: {remainingTime}", Placement.Center, default, 20, default);

            }, null, 1000, 1000);

            while (true)
            {
                Core.WritePositionedString(message, Placement.Center, default, 2, default);

                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);
                    if(input.Key == ConsoleKey.Enter){
                        timer?.Dispose();
                        printTimer?.Dispose();
                        return (remainingTime, null);
                    }
                    else{
                      Core.WritePositionedString("Vous avez pressé la mauvaise touche",Placement.Center,default,1,default);
                    }
                }

                if (timeUp)
                {
                    return (remainingTime, null);
                }
            }
        }



        public static (string, int) TimedEnterInput(int timeLimit, string prompt)
        {
            if(timeLimit==0){
                Console.WriteLine($"Vous n'avez pas de temps limite pour rentrer un mot");
                return (null, 0);
            }
            else if(timeLimit<0){
                Console.WriteLine("Vous avez entré un temps négatif, vous allez sortir du jeu");
                return(null,0);
            }
            Console.WriteLine(prompt);
            string? input = null;
            var task = Task.Run(() =>
            {
                input = Console.ReadLine();
            });

            if (task.Wait(TimeSpan.FromSeconds(timeLimit)))
            {
                return (input, timeLimit - (int)task.Status);
            }
            else
            {
                Core.WritePositionedString("Time is up",Placement.Center,default,20,default);
                return (null, 0);
            }
        }


        /// <summary>
        /// Thei function create the number of player and the timer for each player
        /// </summary>
        static public void CreatePlayer()
        {
            var index = Core.ScrollingNumberSelector("Choisir le timer par joueur :",60,180,60,30);
            int timer = Convert.ToInt32(index.Item2);
            index = Core.ScrollingNumberSelector("Choisir le nombre de joueur :",1,4,1,1);
            int number=0;
            if(index.Item1 == 0){
                number = Convert.ToInt32(index.Item2);
            }
            else{
                Core.WritePositionedString("Vous avez pressé la touche echap, vous allez sortir du jeu, pressez entrée pour confirmer",Placement.Center,default,10,default);
                Core.ExitProgram();
            }
            List<Player> playerlist = new List<Player>();
            PlayerList players = new PlayerList(playerlist);

            for(int i = 0; i < number; i++){
                Core.WritePositionedString($"Entrez le nom du joueur {i+1} : ",Placement.Center,default,10,default);
                string? name = Console.ReadLine();
                name ??= "";
                if(name == "" || name == null){
                    name = "Joueur " + (i+1);
                }
                players.playerlist.Add(new Player {Name = name, Timer = timer, Score = 0, WordList = new List<string>()});
                Core.ClearLine(10);
            }
            players.WriteYAML("data/config.yml");   
            Console.WriteLine("Liste des joueurs : ");
            Console.WriteLine("");
            Console.WriteLine(players.toString());   
        }
        
    }
}