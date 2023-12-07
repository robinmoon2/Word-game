using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CsvHelper.Configuration.Attributes;

namespace Projet_A2_S1
{
    public class Game
    {
        private CustomDictionary dictionary;
        private GameBoard board;
        private PlayerList players;
        public Game()
        {
            dictionary = new CustomDictionary();
            board = new GameBoard();
            List<Player> ListOfPlayer = new List<Player>();
            players = new PlayerList(ListOfPlayer);
            players.ReadYAML("data/config.yml");
        }


        public CustomDictionary Dictionary { get => dictionary; set => dictionary = value; }
        public GameBoard Board { get => board; set => board = value; }
        public PlayerList Players { get => players; set => players = value; }
        

        public bool EndGame(){
            bool endtimer = true;
            bool endword = true;
            //on check les timers : 
            foreach(var player in players.playerlist){
                if(player.Timer!=0){
                    endtimer=false;
                }
            }
            // on check les mots 
            for(int i=0; i<board.Board.GetLength(0);i++){
                for(int j=0; j<board.Board.GetLength(1);j++){
                    if(board.Board[i,j]!=' '){
                        endword=false;
                    }
                }
            }
            return endtimer || endword;
        }


        public void Turn(){
            foreach(var Player in players.playerlist){
                if(Player.Timer == 0){
                    continue;
                }
                Core.WritePositionedString("C'est au tour de "+Player.Name, Placement.Right,default, 10, default);
                Core.WritePositionedString(board.ToString(), Placement.Center, default, 10, default);
                var timer = Method.TimedInput(Player.Timer);
                Player.Timer = timer.Item1;
                if(Player.Timer !=0){
                    var LimitedInput = Method.TimedEnterInput(5, "Entrez un mot : ");
                    var input = LimitedInput.Item1;
                    if(input is not null){
                        if(WordValidate(input.ToUpper(),Player)){
                            Player.Add_Mot(input.ToUpper());
                            Player.Add_Score(Player.Word_Value(input.ToUpper()));
                            Player.Timer = timer.Item1;
                        }
                        players.WriteYAML("data/config.yml");
                    }
                    else{
                        Console.WriteLine("Vous n'avez rien rentré");
                    }
                }
                else{
                    Console.WriteLine("Vous n'avez trouvé dans les temps, dommage ! ");
                }
                Console.WriteLine("Appuyez sur entrée pour continuer");
                Console.ReadLine();
                Core.ClearWindow();
            }
        }



        public bool WordValidate(string word,Player player){
            for(int i=0; i<player.WordList.Count;i++){
                if(player.WordList[i]==word){
                    Core.WritePositionedString("Ce mot est déjà dans votre liste", Placement.Center,default, 22, default);
                    return false;
                }
            }
             if(dictionary.FindWord(word)){
                 
                var dico = new Dictionary<(int,int), char>();
                for (int y = 0; y < board.Board.GetLength(1) ; y++)
                {

                    if(char.ToLower(board.Board[board.Board.GetLength(0) - 1, y]) == char.ToLower(word[0])) // si la lettre est la même que la première lettre du mot (on commence par la dernière ligne du plateau car on cherche le mot à l'envers)
                    {
                        dico = board.GetWord(board.Board.GetLength(0) - 1, y, word.ToLower()); // on lance la recherche du mot
                    }
                }
                board.SaveAndWrite();
                if(dico is null || dico.Count() < word.Length){
                    Core.WritePositionedString("Le mot n'est pas présent sur le plateau", Placement.Center,default, 22, default);
                    return false;
                }
                else{
                    foreach(var item in dico) // on affiche le dictionnaire les coordonnées et les lettres)
                    {
                        board.Board[item.Key.Item1, item.Key.Item2] = ' ';
                    }
                    board.Maj_Plateau(); // on met à jour le tableau 
                    board.SaveAndWrite(); // on sauvegarde le tableau 
                    return true; 
                }
             }
             else{
                Core.WritePositionedString("Le mot n'est pas contenu dans le dictionnaire", Placement.Center,default, 22, default);
                 return false;
             }
        }
    }
}
