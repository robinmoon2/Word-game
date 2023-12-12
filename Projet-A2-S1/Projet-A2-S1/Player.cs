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
    /// This function can create a string of the player's information with his name, timer, score and wordlist  
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
    /// This function add a word passed win parameter in the word list of the player
    /// </summary>
    /// <param name="word">tThe word that we are adding at the wordlist </param>
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
    /// This function take a string in parameter and look if it's in the player's wordlist
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
    /// This function take an int as parameter and add it to the player's score
    /// </summary>
    /// <param name="value"></param>
    public void Add_Score(int value) 
    {
        this.score += value;
    }

    /// <summary>
    /// Function that calculate the value of the word 
    /// </summary>
    /// <param name="word">word that we are looking for</param>
    /// <returns></returns>
    /// 
    public int Word_Value(string word ){
        if( word is null) { return 0; }
        else if(!word.All(char.IsLetter)) { return 0; }
        int value = 0;
        for(int i=0; i<word.Length;i++)
            value += Letter_Value(word[i]);
        return value;
    }
    /// <summary>
    /// This function can calculate the value of a letter according to the file Lettre.txt ( the value of the letter is in the third column of the file)
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


