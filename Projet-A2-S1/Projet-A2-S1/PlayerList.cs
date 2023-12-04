namespace Projet_A2_S1
{
    public class PlayerList
    {
        public List<Player> playerlist { get; set; }


        ///
        public PlayerList(List<Player> playerlist){
            this.playerlist=playerlist;
        }

        /// <summary>
        /// Write the informations of the players in a YAML file
        /// </summary>
        /// <param name="YAML_PATH"> location of the yaml file </param>
        public void WriteYAML(string YAML_PATH){
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlString = serializer.Serialize(playerlist);

            File.WriteAllText(YAML_PATH, yamlString);
        }

        /// <summary>
        /// Read the YAML file and create a list of player
        /// </summary>
        /// <param name="YAML_PATH">Location of the yaml file </param>
        public void ReadYAML(string YAML_PATH)
        {
            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

            var yamlString = File.ReadAllText(YAML_PATH);
            playerlist = deserializer.Deserialize<List<Player>>(yamlString);
        }


        public string toString(){
            string s="";
            foreach(Player player in playerlist){
                s=s+"\n"+player.toString();
            }
            return s;
        }

        public List<string> toStringArray(){
            List<string> s = new ();
            foreach (var player in playerlist){
                s.Add(player.toString());
            }
            return s;
        }

    }


}