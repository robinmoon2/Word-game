using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
namespace Projet_A2_S1;

internal class Dictionnaire
{
    Dictionary<char, List<string>> dictionary;



     public Dictionnaire()
    {
        using (StreamReader reader = new StreamReader("data/Mots_français.txt"))
        {
            this.dictionary = new Dictionary<char, List<string>>(); 
            string line;
            while ((line = reader.ReadLine()) != null)
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
                dictionary[key] = Tri_XXX(dictionary[key]);
            }
        }
        SerializeDictionary();
    }




    public Dictionary<char, List<string>> Dictionary {get { return dictionary;} set {dictionary = value;}}


    public string toString()
    {
        string dico="";
        foreach(KeyValuePair<char,List<string>> parts in dictionary){
            dico += $"{parts.Key} : il y a : {parts.Value.Count} \n mots ";
        }
        return dico; 

    }


    public void SerializeDictionary()
    {
        var stream = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        string JsonString = JsonSerializer.Serialize(dictionary, stream);

        File.WriteAllText("data/Dictionary.Json", JsonString);
    }
    


    public bool FindWord(string mot){
        string jsonString = File.ReadAllText("data/Dictionary.Json");
        var dictionary = JsonSerializer.Deserialize<Dictionary<char,List<string>>>(jsonString);
        mot = mot.ToUpper();
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
    
    public List<string> Tri_XXX(List<string> wordlist) 
    {
        if(wordlist == null || wordlist.Count()<=1){
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
            lower = Tri_XXX(lower);
            greater= Tri_XXX(greater);
            lower.Add(pivot);
            return lower.Concat(greater).ToList();
        }

    }
    

}
