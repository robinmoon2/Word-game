using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Projet_A2_S1;
public class GameBoard
{
    private const string WORKING_FILE = "data/AcutalPlate.csv"; 
    private const string EXAMPLE_FILE = "data/Plate1.csv";
    private const string RANDOM_FILE = "data/Lettre.txt";
    private static readonly Random s_rnd = new();
    public char[,] Board;
    public GameBoard()
    {
        Board ??= new char[8,8];
        var difficultyIndex = Core.ScrollingMenuSelector("Choisissez un plateau", default, default, "Plateau par défaut", "Plateau personnalisé" );
        switch(difficultyIndex.Item1)
        {
            case 0:
                string sourceFile;
                switch(difficultyIndex.Item2)
                {
                    case 0:
                        sourceFile = EXAMPLE_FILE ;
                        GenerateExamplePlate(sourceFile);
                        SaveAndWrite();
                    break;
                    case 1:
                        sourceFile = RANDOM_FILE;
                        GenerateRandomPlate(sourceFile);
                        SaveAndWrite();
                    break;
                }
                break;
            default:
                Console.WriteLine("Vous avez pressé la touche echap, vous allez sortir du jeu, pressez entrée pour confirmer");
                Console.ReadKey();
                Core.ExitProgram();
                break;
        }
    }
    void GenerateExamplePlate(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{path}");
        string[] lines = File.ReadAllLines(path);
        if(lines is not null){
            for(int i = 0; i < 8; i ++){
                string[] parts = lines[i].Split(',');
                for(int j = 0; j < 8; j ++){
                    Board[i, j] = char.Parse(parts[j]);
                }
            }
        }
        else 
            throw new FormatException($"Fichier vide à l'adresse :{path}");
    }
    void GenerateRandomPlate(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{path}");
        var dico = new Dictionary<char, int>();
        string[] lines = File.ReadAllLines(path);
        if(lines is not null)
        {
            for(int i = 0; i < 8; i ++)
            {
                string[] parts = lines[i].Split(',');
                dico.Add(char.Parse(parts[0]), int.Parse(parts[1]));
            }
        }
        else 
            throw new FormatException($"Fichier vide à l'adresse :{path}");
        var aplhabet = "abcdefghijklmopqrstuvwxyz";
        for(int i = 0; i < 8; i ++)
            for(int j = 0; j < 8; j ++)
                Board[i,j] = aplhabet[s_rnd.Next(0, aplhabet.Length)];

    }
    public override string ToString()
    {
        if (!File.Exists(WORKING_FILE))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{WORKING_FILE}");
        string[] file = File.ReadAllLines(WORKING_FILE);
        var plate_string = string.Empty;
        for (int i = 0 ;  i < file.Length ; i ++)
        {
            string[] parts = file[i].Split(',');
            for(int j=0; j<8; j++)
            {
                if(parts[j] is not null)
                    plate_string += "| " + parts[j] + " |";
                
            }
            plate_string += "\n"; 
        }
        return plate_string;
    }
    public void Read()
    {
        if (!File.Exists(WORKING_FILE))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{WORKING_FILE}");
        string[] lines = File.ReadAllLines(WORKING_FILE); 
        if(lines is not null){
            for(int i = 0; i < 8; i++)
            {
                string[] parts = lines[i].Split(',');
                for(int j = 0; j < 8; j++)
                {
                    Board[i,j] = char.Parse(parts[j]);
                }
            }
        }
        else 
            throw new FormatException($"Fichier vide à l'adresse :{WORKING_FILE}");
    }
    
    public void SaveAndWrite()  
    {
        using var reader = new StreamWriter(WORKING_FILE);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                reader.Write(Board[i,j]);
                if (j != 7)
                {
                    reader.Write(",");
                }
            }
            reader.WriteLine();
        }
    }

    public Dictionary<(int,int), char>? GetWord(int x, int y, string word, int index = 0, Dictionary<(int, int), char>? dico = null)
    {
        dico ??= new Dictionary<(int,int), char>();
        if (word.Length == index)
            return dico;
        else if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            if (Board[x,y] == word[index])
            {
                if (!dico.ContainsKey((x,y)))
                {
                    dico.Add((x,y), Board[x,y]);
                    if (GetWord(x-1, y, word, index+1, dico) != null
                        ||GetWord(x, y+1, word, index+1, dico) != null
                        //|| Recherche_Mot(x+1, y, word, index+1, dico) != null
                        || GetWord(x, y-1, word, index+1, dico) != null
                        || GetWord(x-1, y-1, word, index+1, dico) != null
                        || GetWord(x+1, y+1, word, index+1, dico) != null
                        || GetWord(x-1, y+1, word, index+1, dico) != null
                        || GetWord(x+1, y-1, word, index+1, dico) != null)
                    {
                        return dico;
                    }
                    dico.Remove((x,y));
                }
            }
        }
        return null;
    }


    

    public void Maj_Plateau()
    {
        for (int i = 0; i < Board.GetLength(1); i++){
            for (int j = 0; j < Board.GetLength(0); j++){
                if (Board[i,j] == ' ')
                {
                    for (int k = i; k > 0; k --)
                    {

                        Board[k,j] = Board[k-1,j];
                    }
                    Board[0,j] = ' ';
                }
            }
        }
    }
}


