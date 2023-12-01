namespace Projet_A2_S1;

public class Player
{
     string name;
     int timer;
     int score;

     List<string> wordList;
    

    public string Name{get{return name;} set{name = value;}}
    public int Timer{get{return timer;} set{timer = value;}}
    public int Score{get{return score;} set{score = value;}}
    public List<string> WordList{get{return wordList;} set{wordList = value;}}




    public string toString(){
        string playerString= $"Name : {name}\n Timer : {timer}\n Score : {score} \n WordList :";
        foreach(string word in wordList){
            playerString += $"\n {word}";
        }
        return playerString;
    }



    public void Add_Mot (string mot) 
    {
        if(!Contient(mot) ){ // ! il faut mettre si le mot existe dans le dictionnaire ou alors on fait le check avant dans le program.cs
            wordList.Add(mot);
            Console.WriteLine("Mot ajoué, bravo ! ");
        }
        else{
            Console.WriteLine("Mot déjà dans votre liste ! ");
        }

    }


    public bool Contient (string mot) 
    {
        bool verif = false;
        for(int i=0; i<wordList.Count; i++)
        {
            if(wordList[i] == mot)
            {
                verif = true;
            }
        }
        return verif;
    }

    public void Add_Score(int val) 
    {

    }

}


