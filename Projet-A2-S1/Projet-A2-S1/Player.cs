namespace Projet_A2_S1;

public class Player
{
     string? name;
     int timer;
     int score;
     List<string>? wordList;
    

    public string? Name{get{return name;} set{name = value;}}
    public int Timer{get{return timer;} set{timer = value;}}
    public int Score{get{return score;} set{score = value;}}
    public List<string>? WordList{get{return wordList;} set{wordList = value;}}



    /// <summary>
    /// Function that return the information of a player
    /// </summary>
    /// <returns></returns>
    public string toString(){
        string playerString= $"Name : {name}\n Timer : {timer}\n Score : {score} \n WordList :";
        if(wordList is not null){
            foreach(string word in wordList){
                playerString += $"\n {word}";
            }
        }
        else{
            playerString += "\n null";
        }
        return playerString;
    }

    /// <summary>
    /// Function that add a word to the wordlist of the player 
    /// </summary>
    /// <param name="word">the word that we are adding </param>
    public void Add_Mot (string word) 
    {
        if(!Contient(word) && word is not null && wordList is not null){
            wordList.Add(word);
            Console.WriteLine("Mot ajoué, bravo ! ");
        }
        else{
            Console.WriteLine("Mot déjà dans votre liste ! ");
        }

    }
    /// <summary>
    /// Function that look if the word is already in the player's wordlist
    /// </summary>
    /// <param name="word"> the word that we are looking for</param>
    /// <returns></returns>
    public bool Contient (string word) 
    {
        bool verif = false;
        if(wordList is null){
            return false;
        }
        for(int i=0; i<wordList.Count; i++)
        {
            if(wordList[i] == word)
            {
                verif = true;
            }
        }
        return verif;
    }
    /// <summary>
    /// Add the value of the world in the player score 
    /// </summary>
    /// <param name="value"></param>
    public void Add_Score(int value) 
    {
        Core.WritePositionedString("Score ajouté, bravo ! "+ value, Placement.Right, default, 20, default);
        this.score += value;
    }

    /// <summary>
    /// Function that calculate the value of the word 
    /// </summary>
    /// <param name="word">word that we are looking for</param>
    /// <returns></returns>
    public int Word_Value(string word ){
        int value = 0;
        for(int i=0; i<word.Length;i++)
            value += Letter_Value(word[i]);
        Core.WritePositionedString("valeur de "+word+" : "+value, Placement.Right, default, 15, default);
        return value;
    }
    /// <summary>
    /// Function that return the value of the letter
    /// </summary>
    /// <param name="letter">the letter that we are looking for</param>
    /// <returns></returns>
    public static int Letter_Value(char letter)
    {
        using (StreamReader reader = new StreamReader("Lettre.txt"))
        {
            string line = reader.ReadLine() ?? "" ;
            while (line is not null)
            {
                string[] parts = line.Split(',');
                if (parts[0] == letter.ToString().ToUpper())
                {
                    return int.Parse(parts[2]);
                }
                line = reader.ReadLine() ?? "";
            }
        }
        return 0;
    }   
}


