namespace Projet_A2_S1;
public class CustomDictionary
{
    
    private const string TXT_DICTIONARY_PATH = "Mots_Français.txt";
    private const string JSON_DICTIONARY_PATH = "Dictionary.Json";
    public Dictionary<char, List<string>> Dict = new();

    /// <summary>
    /// Constructor of the class CustomDictionary
    /// </summary>
    /// <exception cref="Exception"></exception>
    /// <exception cref="FileNotFoundException">Exception if the file is not found</exception>
    /// <exception cref="NullReferenceException"></exception>
    public CustomDictionary()
    {
        string[] lines;
        if (File.Exists(JSON_DICTIONARY_PATH)){
            string jsonString = File.ReadAllText(JSON_DICTIONARY_PATH);
            Dict = JsonSerializer.Deserialize<Dictionary<char,List<string>>>(jsonString) ?? throw new Exception("Error in deserialization.");}
        else if (!File.Exists(TXT_DICTIONARY_PATH))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{TXT_DICTIONARY_PATH}");
        else 
        {
            lines = File.ReadAllLines(TXT_DICTIONARY_PATH);
            foreach(var line in lines)
            {
                string[] words = line.Split(' ');
                char key = ' ';
                foreach (var word in words)
                {
                    key = word[0];
                    if (!Dict.ContainsKey(key))
                        Dict[key] = new List<string>();
                    Dict[key].Add(word);
                }
                if (Dict[key] is not null)
                    Dict[key] = Sort(Dict[key]);
                else
                    throw new NullReferenceException("Le dictionnaire est null.");
            }
            SerializeDictionary();
        }
    }

    /// <summary>
    /// Function that write the dictionary in a JSON file to stock it 
    /// </summary>
    private void SerializeDictionary()
    {
        var stream = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string JsonString = JsonSerializer.Serialize(Dict, stream);
        File.WriteAllText(JSON_DICTIONARY_PATH, JsonString);
    }


    /// <summary>
    /// This function try to find a word in the dictionary
    /// </summary>
    /// <param name="word">The string that represent the word that we are searching in the dictionary ( wich is stock in dictionary.Json)</param>
    /// <returns></returns>
    /// <exception cref="Exception">If the file is not found or if we can't read it we throw an exception </exception>

    public bool FindWord(string word)
    {
        if(word is null || word == ""){
            return false;
        }
        string jsonString = File.ReadAllText("Dictionary.Json");
        var dictionary = JsonSerializer.Deserialize<Dictionary<char,List<string>>>(jsonString) ?? throw new Exception("Error in deserialization.");
        word = word.ToUpper();
        if(word is  null){
            return false;
        }
        if(dictionary.ContainsKey(word[0]))
        {
            return RechDichoRecursif(word.ToUpper(),dictionary[word[0]]);
        }
        else{
            Console.WriteLine("Mot invalide ");
            return false;
        }
    }

    /// <summary>
    /// This function search a word in a list of string
    /// </summary>
    /// <param name="word"> word that we are looking for </param>
    /// <param name="wordlist">the list of string where we are searching </param>
    /// <returns></returns>
    public bool RechDichoRecursif(string word, List<string> wordlist)
    {
        if(wordlist.Count<1){
            return false;
        }
        else{
            int middle = wordlist.Count/2;
            if(wordlist[middle].ToUpper() == word)
            {
                return true;
            }
            else if ( (string.Compare(wordlist[middle].ToUpper(), word)) < 0){
                return RechDichoRecursif(word, wordlist.GetRange(middle+1,wordlist.Count-middle-1)); // We start at middle+1 because we have already try it
            }
            else{
                return RechDichoRecursif(word, wordlist.GetRange(0,middle));
            }
            
                
        }
    }
    

    /// <summary>
    /// This function sort a list of string with the quick_sort method 
    /// </summary>
    /// <param name="wordlist">the list that we want to sort</param>
    /// <returns></returns>
    public List<string> Sort(List<string> wordlist) 
    {
        if(wordlist is null || wordlist.Count()<=1 ){
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



    /// <summary>
    /// Function that return the string that tell how many words per letter exist in the dictionary
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = string.Empty;
        foreach(KeyValuePair<char, List<string>> parts in Dict)
            str += $"{parts.Key} : il y a {parts.Value.Count} mots \n";
        return str; 
    }
}
