using System.Runtime.Serialization;

namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            Method.CreatePlayer();

            var playerList = new List<Player>();
            PlayerList players = new PlayerList(playerList);
            
            Dictionnaire dico = new Dictionnaire();

            players.ReadYAML("data/config.yml");
            var index = Core.ScrollingMenuSelector("Voulez vous modifiez un nom ? ",default , default, "Oui","Non");
            if(index.Item2 == 0){
                Core.WritePositionedString("Quel pseudo modifier ? ",Placement.Left,default,10,default);
                string nameModif = Console.ReadLine() ?? "";
                Player? Modif = players.playerlist.FirstOrDefault(player => player.Name == nameModif);
                if (Modif != null)
                {
                    Modif.Name = "test";
                    players.WriteYAML("data/config.yml");
                }
                else{
                    Console.WriteLine("Le pseudo n'existe pas");
                }
                Core.ClearWindow();
            }
            Core.WritePositionedString(players.toString(),Placement.Right,default,10,default);
            
            while(players.playerlist.Any(player => player.Timer != 0)){
                foreach (var player in players.playerlist)
                {
                    Core.WritePositionedString("Joueur : "+player.Name+", à vous de jouer. Press",Placement.Right,default,10,default);
                    Core.WritePositionedString("Entrée pour commencer",Placement.Right,default,11,default);
                    bool end_turn = false;
                    while(!end_turn){
                        var turn = Method.TimedNumberInput(player.Timer, "Entrez un mot : ");
                        player.Timer = turn.Item1;
                        if(player.Timer == 0){
                            Core.WritePositionedString("Temps écoulé",Placement.Right,default,12,default);
                            end_turn = true;
                        }
                        else{
                                string? input = turn.Item2;
                            Console.WriteLine(input);
                            Console.WriteLine(dico.ToString());
                            if(dico.FindWord(input))
                            {
                                if(!player.Contient(input)){
                                    player.Add_Mot(input);
                                    Console.WriteLine(player.Word_Value(input));
                                    player.Add_Score(player.Word_Value(input));
                                    end_turn = true;
                                }
                                
                            }
                            else
                            {
                                Core.WritePositionedString("Le mot n'existe pas",Placement.Right,default,12,default);
                            }
                        }
                        players.WriteYAML("data/config.yml");
                        players.ReadYAML("data/config.yml");
                    }
                    Core.ClearWindow();
                    Console.WriteLine(players.toString());
                    

                }
            }
        }
    }
}

