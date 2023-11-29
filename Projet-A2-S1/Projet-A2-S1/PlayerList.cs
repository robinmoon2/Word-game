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


        public void WriteYAML(string YAML_PATH){
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlString = serializer.Serialize(playerlist);

            File.WriteAllText(YAML_PATH, yamlString);
        }
        public void ReadYAML(string YAML_PATH)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yamlString = File.ReadAllText(YAML_PATH);
            var yamlDictionary = deserializer.Deserialize<Dictionary<string, List<Player>>>(yamlString);
            if (yamlDictionary.ContainsKey("playerlist"))
            {
                playerlist = yamlDictionary["playerlist"];
            }
            else
            {
                throw new KeyNotFoundException("'playerlist' not found in the YAML file.");
            }
        }
    }


}