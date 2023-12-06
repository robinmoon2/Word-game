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
        }

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
                Core.WritePositionedString("C'est au tour de "+Player.Name, Placement.Left,default, 10, default);
                Core.WritePositionedString("Il vous reste "+Player.Timer+" secondes", Placement.Left,default, 11, default);
                Core.WritePositionedString(board.ToString(), Placement.Right,default, 10, default);
                Method.TimedNumberInput(Player.Timer);
                if(Player.Timer !=0){
                    Console.WriteLine("Rentrez un mot : ");
                    var input = Console.ReadLine();
                    if(input is not null){
                        if(WordValidate(input.ToUpper(),Player)){
                            Player.Add_Mot(input.ToUpper());
                            Player.Add_Score(input.Length);
                        }
                    }
                    else{
                        Console.WriteLine("Vous n'avez rien rentré");
                    }

                }
                else{
                    Console.WriteLine("Vous n'avez trouvé dans les temps, dommage ! ");
                }
            }
        }



        public bool WordValidate(string word,Player player){
            for(int i=0; i<player.WordList.Count;i++){
                if(player.WordList[i]==word){
                    Console.WriteLine("ce mot est déjà dans votre liste");
                    return false;
                }
            }
             if(dictionary.FindWord(word)){
                 
                    var dico = new Dictionary<(int,int), char>();
                    for (int y = 0; y < board.Board.GetLength(1) ; y++)
                {
                    if(board.Board[board.Board.GetLength(0) - 1, y] == word[0]) // si la lettre est la même que la première lettre du mot (on commence par la dernière ligne du plateau car on cherche le mot à l'envers)
                    {
                        dico = board.GetWord(board.Board.GetLength(0) - 1, y, word); // on lance la recherche du mot
                    }
                }
                if(dico is null || dico.Count != word.Length){
                    Console.WriteLine("Le mot n'est pas présent sur le plateau");
                    return false;
                }
                else{
                    board.SaveAndWrite();
                    foreach(var item in dico) // on affiche le dictionnaire les coordonnées et les lettres)
                    {
                        board.Board[item.Key.Item1, item.Key.Item2] = ' ';
                    }
                    board.Maj_Plateau();
                    board.SaveAndWrite();
                    Console.WriteLine(board);
                    return true; 
                }
             }
             else{
                Console.WriteLine("Le mot n'est pas contenu dans le dictionnaire");
                 return false;
             }
        }

    }
}
