using YamlDotNet.Core;

namespace Projet_A2_S1
{
    public class Method
    {
        /// <summary>
        /// This function create the main menu of the game
        /// </summary>
        static public void main_menu()
        {
            Core.ClearWindow();
            Core.SetTitle("Mots Glissants");
            Core.WriteFullScreen("Mots Glissants", true, ("Jeu des mots", "", "(pas du tout comme candy crush)"), ("Hugo Peltier", "TDO", "Robin L'hyver"));
            bool EndMenu = true;
            while (EndMenu)
            {
                var index = Core.ScrollingMenuSelector(" Menu Principal :", default, default, "Jouer", "Changer de couleur","Crédit", "Quitter le jeu");
                switch (index.Item1)
                {

                    case 0:
                        switch (index.Item2)
                        {

                            case 0:
                                EndMenu = false;

                                break;

                            case 1:
                                var color = Core.ScrollingMenuSelector("Choisir une couleur :", default, default, "Rouge", "Bleu", "Vert", "Jaune", "Blanc", "Noir");
                                ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Black, ConsoleColor.Gray };
                                var couleur = colors[color.Item2];
                                Core.ChangeForeground(couleur);
                                Core.WritePositionedString("Couleur choisie : ", Placement.Center, default, 10, default);
                                break;

                            case 2:
                                string s = "MIT License\r\n\r\nCopyright (c) 2023 PELTIER/L'HYVER/PROJET1\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy\r\nof this software and associated documentation files (the \"Software\"), to deal\r\nin the Software without restriction, including without limitation the rights\r\nto use, copy, modify, merge, publish, distribute, sublicense, and/or sell\r\ncopies of the Software, and to permit persons to whom the Software is\r\nfurnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all\r\ncopies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\nAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\nOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\r\nSOFTWARE.";
                                Core.WriteContinuousString(s, 10);
                                Core.WriteContinuousString("Pressez entree pour sortir ", 35);
                                Console.ReadKey();
                                Core.ClearWindow();
                                break;
                            case 3:
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
        /// Function that time the turn of the player. Its stop when the player press enter of when the timer == 0 
        /// </summary>
        /// <param name="timeLimit"> Time limit of the turn</param>
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
                Core.WritePositionedString("Time is up", Placement.Center, default, 20, default);
            }, null, timeLimit * 1000, Timeout.Infinite);

            printTimer = new Timer((state) =>
            {
                remainingTime--;
                Core.WritePositionedString($"Remaining time: {remainingTime}", Placement.Right, default, 20, default);

            }, null, 1000, 1000);

            while (true)
            {
                Core.WritePositionedString(message, Placement.Center, default, 2, default);

                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true);
                    if (input.Key == ConsoleKey.Enter)
                    {
                        timer?.Dispose();
                        printTimer?.Dispose();
                        return (remainingTime, null);
                    }
                    else
                    {
                        Core.WritePositionedString("Vous avez pressé la mauvaise touche", Placement.Center, default, 1, default);
                    }
                }

                if (timeUp)
                {
                    return (remainingTime, null);
                }
            }
        }


        /// <summary>
        /// Function that is used to time for a player to write a word and stop when the key Enter is pressed with an input or the timer == 0 
        /// </summary>
        /// <param name="timeLimit">the time limit to write the word </param>
        /// <param name="prompt">The message that we write to the user </param>
        /// <returns></returns>
        public static (string?, int) TimedEnterInput(int timeLimit, string prompt)
        {
            if (timeLimit == 0)
            {
                Console.WriteLine($"Vous n'avez pas de temps limite pour rentrer un mot");
                return (null, 0);
            }
            else if (timeLimit < 0)
            {
                Console.WriteLine("Vous avez entré un temps négatif, vous allez sortir du jeu");
                return (null, 0);
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
                Core.WritePositionedString("Time is up", Placement.Right, default, 20, default);
                return (null, 0);
            }
        }


        /// <summary>
        /// This function create the number of player and the timer for each player
        /// </summary>
        static public void CreatePlayer()
        {
            var index = Core.ScrollingNumberSelector("Choisir le timer par joueur :", 60, 180, 60, 30);
            int timer = Convert.ToInt32(index.Item2);
            index = Core.ScrollingNumberSelector("Choisir le nombre de joueur :", 1, 4, 1, 1);
            int number = 0;
            if (index.Item1 == 0)
            {
                number = Convert.ToInt32(index.Item2);
            }
            else
            {
                Core.WritePositionedString("Vous avez pressé la touche echap, vous allez sortir du jeu, pressez entrée pour confirmer", Placement.Center, default, 10, default);
                Core.ExitProgram();
            }
            List<Player> playerlist = new List<Player>();
            PlayerList players = new PlayerList(playerlist);

            for (int i = 0; i < number; i++)
            {
                Core.WritePositionedString($"Entrez le nom du joueur {i + 1} : ", Placement.Center, default, 10, default);
                string? name = Console.ReadLine();
                name ??= "";
                if (name == "" || name == null)
                {
                    name = "Joueur " + (i + 1);
                }
                players.playerlist.Add(new Player { Name = name, Timer = timer, Score = 0, WordList = new List<string>() });
                Core.ClearLine(10);
            }
            players.WriteYAML("config.yml");
            Console.WriteLine("Liste des joueurs : ");
            Console.WriteLine("");
            Console.WriteLine(players.toString());
        }

    }
}