using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Projet_A2_S1
{
    public class PlayerList
    {
        public List<Player> playerlist { get; set; }


        public void toString(){
            foreach(Player player in playerlist){
                Console.WriteLine(player.Name);
            }
        }
    }


}