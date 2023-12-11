using System.Diagnostics;

namespace Projet_A2_S1
{
    public class Game
    {
        /// <summary>
        /// This class build the game during the running program
        /// One run is equal to one game
        /// </summary>
        private CustomDictionary dictionary;
        private GameBoard board;
        private PlayerList players;
        public Game()
        {
            dictionary = new CustomDictionary();
            board = new GameBoard();
            List<Player> ListOfPlayer = new List<Player>();
            players = new PlayerList(ListOfPlayer);
            players.ReadYAML("config.yml");
        }
        /// <summary>
        /// Second constructor for the test function
        /// </summary>
        /// <param name="test"></param>
        public Game(bool test)
        {
            dictionary = new CustomDictionary();
            board = new GameBoard(test);
            List<Player> ListOfPlayer = new List<Player>();
            players = new PlayerList(ListOfPlayer);
            players.ReadYAML("config.yml");
        }




        public CustomDictionary Dictionary { get => dictionary; set => dictionary = value; }
        public GameBoard Board { get => board; set => board = value; }
        public PlayerList Players { get => players; set => players = value; }

        /// <summary>
        /// This function can tel if the game is over or not 
        /// </summary>
        /// <returns></returns>
        public bool EndGame()
        {
            bool endtimer = true;
            bool endword = true;
            //on check les timers : 
            foreach (var player in players.playerlist)
            {
                if (player.Timer != 0)
                {
                    endtimer = false;
                }
            }
            // on check les mots 
            for (int i = 0; i < board.Board.GetLength(0); i++)
            {
                for (int j = 0; j < board.Board.GetLength(1); j++)
                {
                    if (board.Board[i, j] != ' ')
                    {
                        endword = false;
                    }
                }
            }
            return endtimer || endword;
        }

        /// <summary>
        /// This function can pass the turn over each players if their timer !=0 
        /// </summary>
        public void Turn()
        {
            foreach (var Player in players.playerlist)
            {
                if (Player.Timer == 0)
                {
                    continue;
                }
                Core.WritePositionedString("C'est au tour de " + Player.Name, Placement.Right, default, 10, default);
                Core.WritePositionedString("Score : " + Player.Score, Placement.Right, default, 11, default);
                int cmp = 0;
                foreach (string word in Player.WordList)
                {
                    Core.WritePositionedString("-" + word, Placement.Right, default, 12 + cmp, default);
                    cmp++;
                }

                Core.WritePositionedString(board.ToString(), Placement.Left, default, 10, default);
                var timer = Method.TimedInput(Player.Timer);
                //temps global -= timer.Item1;
                //On change EndGame avec le temps global = 0 
                Player.Timer = timer.Item1;
                if (Player.Timer != 0)
                {
                    var LimitedInput = Method.TimedEnterInput(5, "Entrez un mot : ");
                    var input = LimitedInput.Item1;
                    if (input is not null)
                    {
                        if (WordValidate(input.ToUpper(), Player))
                        {
                            Core.WritePositionedString("Mot ajouté ! Bravo : " + Player.Word_Value(input), Placement.Right, default, 20, default);
                            Player.Add_Mot(input.ToUpper());
                            Player.Add_Score(Player.Word_Value(input.ToUpper()));
                            Player.Timer = timer.Item1;
                        }
                        players.WriteYAML("config.yml");
                    }
                    else
                    {
                        Core.WritePositionedString("Vous n'avez rien rentré", Placement.Right, default, 20, default);
                    }
                }
                else
                {
                    Core.WritePositionedString("Vous n'avez trouvé dans les temps, dommage ! ", Placement.Right, default, 20, default);
                }

                Core.WritePositionedString("Appuyez sur entrée pour continuer", Placement.Right, default, 23, default);
                Console.ReadLine();
                Core.ClearWindow();
            }
        }

        /// <summary>
        /// Print the results of the game
        /// </summary>
        public void Results()
        {
            Core.WritePositionedString("Voici les résultats : ", Placement.Center, default, 10, default);
            players.playerlist.Sort((x, y) => y.Score.CompareTo(x.Score));
            int cmp = 0;
            foreach (var player in players.playerlist)
            {
                Core.WritePositionedString(player.Name + " : " + player.Score, Placement.Center, default, 11+cmp, default);
                cmp++;
            }
            Core.WritePositionedString("Appuyez sur entrée pour continuer", Placement.Center, default, 23 + cmp, default);

            Console.ReadLine();
            Core.ClearWindow();
        }


        /// <summary>
        /// This function look if the word is not in the list of the player, if it's in the dictionary and if the word is on the board
        /// </summary>
        /// <param name="word"> the word that we are looking for </param>
        /// <param name="player">The player that is searching the word</param>
        /// <returns></returns>
        public bool WordValidate(string word, Player player)
        {
            for (int i = 0; i < player.WordList.Count; i++)
            {
                if (player.WordList[i] == word)
                {
                    Core.WritePositionedString("Ce mot est déjà dans votre liste", Placement.Right, default, 20, default);
                    return false;
                }
            }
            if (dictionary.FindWord(word))
            {

                var dico = new Dictionary<(int, int), char>();
                for (int y = 0; y < board.Board.GetLength(1); y++)
                {
                    if (char.ToLower(board.Board[board.Board.GetLength(0) - 1, y]) == char.ToLower(word[0])) // si la lettre est la même que la première lettre du mot (on commence par la dernière ligne du plateau car on cherche le mot à l'envers)
                    {
                        dico = board.GetWord(board.Board.GetLength(0) - 1, y, word.ToLower()); // on lance la recherche du mot
                        if (dico is not null)
                        {
                            y = board.Board.GetLength(1);
                        }
                    }
                }
                board.SaveAndWrite();
                if (dico is null || dico.Count() < word.Length)
                {
                    Core.WritePositionedString("Le mot n'est pas présent sur le plateau", Placement.Right, default, 20, default);
                    return false;
                }
                else
                {
                    foreach (var item in dico) // on affiche le dictionnaire les coordonnées et les lettres)
                    {
                        board.Board[item.Key.Item1, item.Key.Item2] = ' ';
                    }
                    board.Maj_Plateau(); // on met à jour le tableau 
                    board.SaveAndWrite(); // on sauvegarde le tableau 
                    return true;
                }
            }
            else
            {
                Core.WritePositionedString("Le mot n'est pas contenu dans le dictionnaire", Placement.Right, default, 20, default);
                return false;
            }
        }
    }
}
