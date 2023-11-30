namespace Projet_A2_S1;

public class Player
{
     string name;
     int timer;
     int score;
    //string name="", int timer=0, int score=0
    public Player(){
        this.name = name;
        this.timer=timer;
        this.score = score;
    }

    public string Name{get{return name;} set{name = value;}}
    public int Timer{get{return timer;} set{timer = value;}}
    public int Score{get{return score;} set{score = value;}}


    public string toString(){
        return $"Name : {name}\n Timer : {timer}\n Score : {score}";
    }

}
/*
public void Add_Mot (string mot) 
{
}
public string toString()
{

}
public void Add_Score(int val) 
{

}
public bool Contient (string mot) 
{
    
}


*/
