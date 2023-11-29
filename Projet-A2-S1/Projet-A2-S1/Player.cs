namespace Projet_A2_S1;

public class Player
{
     string name;
     int timer;
     int score;

    public Player(string name, int timer, int score ){
        this.name = name;
        this.timer=timer;
        this.score = score;
    }

    public string Name{get{return name;}}
    public int Timer{get{return timer;}}
    public int Score{get{return score;}}

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
