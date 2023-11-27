namespace Projet_A2_S1;

internal class Joueur
{
    string nom;
    int score;
    List<string> mots;
    
    int chrono;

    ConsoleColor color; // couleur du joueur
    public Joueur(string nom,int chrono, ConsoleColor color)
    {
        this.nom = nom;
        this.score = 0;
        this.mots = new List<string>();
        this.chrono = chrono;
        this.color = color;
    }
    string Nom { get => nom; }
    int Score { get => score; }
    List<string> Mots { get => mots; }
    int Chrono { get => chrono; }

    public string toString()
    {
        return $"Nom : {this.nom} \nScore : {this.score}     Timer : {chrono}\nMots : {this.mots}";
    }
}
/*
public void Add_Mot (string mot) 
{
    
}

public void Add_Score(int val) 
{

}
public bool Contient (string mot) 
{
    
}

}
*/
