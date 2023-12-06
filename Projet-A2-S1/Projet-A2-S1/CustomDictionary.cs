namespace Projet_A2_S1;
class CustomDictionary
{
    private const string TXT_DICTIONARY_PATH = "data/Mots_Français.txt";
    private const string JSON_DICTIONARY_PATH = "data/Dictionary.Json";
    public Dictionary<char, List<string>> dictionary = new();
    public CustomDictionary()
    {
        string[] lines = File.ReadAllLines(TXT_DICTIONARY_PATH);
        foreach(var line in lines)
        {
            string[] words = line.Split(' ');
            char key = ' ';
            foreach (var word in words)
            {
                key = word[0];
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = new List<string>();
                }
                dictionary[key].Add(word);
            }
            if (dictionary[key] is not null)
                dictionary[key] = Sort(dictionary[key]);
            else
                throw new NullReferenceException("Le dictionnaire est null.");
        }
        SerializeDictionary();
    }
    private void SerializeDictionary()
    {
        var stream = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string JsonString = JsonSerializer.Serialize(dictionary, stream);
        File.WriteAllText(JSON_DICTIONARY_PATH, JsonString);
    }
    public Dictionary<char, List<string>> Dictionary {get { return dictionary;} set {dictionary = value;}}
    
    public bool FindWord(string mot){
        if(mot is null || mot == ""){
            return false;
        }
        string jsonString = File.ReadAllText("data/Dictionary.Json");
        var dictionary = JsonSerializer.Deserialize<Dictionary<char,List<string>>>(jsonString);
        mot = mot.ToUpper();
        if( mot == null){
            return false;
        }
        if(dictionary.ContainsKey(mot[0])){
            return RechDichoRecursif(mot.ToUpper(),dictionary[mot[0]]);
        }
        else{
            Console.WriteLine("Mot invalide ");
            return false;
        }
    }

    public bool RechDichoRecursif(string mot, List<string> wordlist)
    {
        if(wordlist.Count<1){
            return false;
        }
        else{
            int middle = wordlist.Count/2;
            if(wordlist[middle].ToUpper() == mot){
                return true;
            }
            else if ( (string.Compare(wordlist[middle].ToUpper(),mot)) < 0){
                return RechDichoRecursif(mot,wordlist.GetRange(middle+1,wordlist.Count-middle-1)); // on commence à middle+1 car on a déjà testé le mot à la position middle voir def GEtRange  
            }
            else{
                return RechDichoRecursif(mot,wordlist.GetRange(0,middle));
            }
            
                
        }
    }
    
    public List<string> Sort(List<string> wordlist) 
    {
        if(wordlist == null || wordlist.Count()<=1 || wordlist[0] == null){
            return wordlist;
        }
        else{
            var pivot = wordlist[0];
            var lower = new List<string>();
            var greater = new List<string>();
            for(int i=1; i<wordlist.Count();i++){
                int compare = string.Compare(wordlist[i],pivot);
                if(compare <0){
                    lower.Add(wordlist[i]);
                }
                else{
                    greater.Add(wordlist[i]);
                }
            }
            lower = Sort(lower);
            greater= Sort(greater);
            lower.Add(pivot);
            return lower.Concat(greater).ToList();
        }

    }
    public override string ToString()
    {
        string str = string.Empty;
        foreach(KeyValuePair<char, List<string>> parts in dictionary)
            str += $"{parts.Key} : il y a {parts.Value.Count} mots \n";
        return str; 
    }
}
