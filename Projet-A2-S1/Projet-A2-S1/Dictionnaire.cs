using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.IO;
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

                foreach (var word in words)
                {
                    char key = word[0];

                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary[key] = new List<string>();
                    }
                    dictionary[key].Add(word);
                }
            }
        }
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

    /*
    public bool RechDichoRecursif(string mot)
    {

    }
    public void Tri_XXX() 
    {

    }
    */

}
