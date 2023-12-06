using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Projet_A2_S1;
public class Plateau
{
    private const string WORKING_FILE = "data/AcutalPlate.csv"; 
    private const string EXAMPLE_FILE = "data/Plate1.csv";
    private const string RANDOM_FILE = "data/Lettre.txt";
    private static readonly Random s_rnd = new();
    public char[,] Plate;
    public Plateau()
    {
        Plate ??= new char[8,8];
        var difficultyIndex = Core.ScrollingMenuSelector("Choisissez un plateau", default, default, "Plateau par défaut", "Plateau personnalisé" );
        switch(difficultyIndex.Item1){
            case 0:
                string sourceFile;
                switch(difficultyIndex.Item2){
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
    void GenerateExamplePlate(string path){
        if (!File.Exists(path))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{path}");
        string[] lines = File.ReadAllLines(path);
        if(lines is not null){
            for(int i = 0; i < 8; i ++){
                string[] parts = lines[i].Split(',');
                for(int j = 0; j < 8; j ++){
                    Plate[i, j] = char.Parse(parts[j]);
                }
            }
        }
        else 
            throw new FormatException($"Fichier vide à l'adresse :{path}");
    }
    void GenerateRandomPlate(string path){
        if (!File.Exists(path))
            throw new FileNotFoundException($"Aucun fichier à l'adresse :{path}");
        var dico = new Dictionary<char, int>();
        string[] lines = File.ReadAllLines(path);
        if(lines is not null){
            for(int i = 0; i < 8; i ++){
                string[] parts = lines[i].Split(',');
                dico.Add(char.Parse(parts[0]), int.Parse(parts[1]));
            }
        }
        else 
            throw new FormatException($"Fichier vide à l'adresse :{path}");
        
        for(int i = 0; i < 8; i ++)
        {
            for(int j = 0; j < 8; j ++)
            {
                // Operation to generate a random letter
                var keys = dico.Keys.ToList();
                char key = keys[s_rnd.Next(0, keys.Count)];

                if(dico[key] != 0)
                {
                    Plate[i,j] = keys[s_rnd.Next(0, keys.Count)];
                    dico[key] -= 1;
                }
                else
                    j--;
            }
        }
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
            for(int i=0; i<8; i++){
                string[] parts = lines[i].Split(',');
                for(int j=0; j<8; j++){
                    Plate[i,j] = char.Parse(parts[j]);
                }
            }
        }
        else{
            Console.WriteLine("Le fichier n'existe pas");
        }
    }
    public void SaveAndWrite() 
    {
        using var reader = new StreamWriter(WORKING_FILE);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                reader.Write(Plate[i, j]);
                if (j != 7)
                {
                    reader.Write(",");
                }
            }
            reader.WriteLine();
        }
    }

    public Dictionary<(int,int), char> Recherche_Mot(int x, int y, string word, int index = 0, Dictionary<(int,int),char>? dico = null)
    {
        if(dico is null){
            dico = new Dictionary<(int,int), char>();
            Recherche_Mot(x,y,word,index,dico);
        }
        if(word.Length == index){
            return dico;
        }
        else if(x>=0 || x<=7 || y>=0 || y<=7){
            if(Plate[x,y] == word[index]){
                if(!dico.ContainsKey((x,y))){
                    Recherche_Mot(x-1,y,word,index+1,dico);
                    Recherche_Mot(x,y+1,word,index+1,dico);
                }
                return dico;
            }
            if(Plate[x,y] != word[index]){
                return dico;
            }
        }
        return dico;
    }
}



/*
public void Maj_Plateau(object objet)
{
    
}

}
*/
