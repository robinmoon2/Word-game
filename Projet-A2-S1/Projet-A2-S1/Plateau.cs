using System.Collections;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators;

namespace Projet_A2_S1;

public class Plateau
{
    string path;

    char[,] plate;



    public Plateau(string PATH="")
    {
        var difficulty_idex = Core.ScrollingMenuSelector("Choisissez un plateau",default,default, "Plateau par défaut", "Plateau personnalisé" );
        switch(difficulty_idex.Item1){
            case 0:
                switch(difficulty_idex.Item2){
                    case 0:
                        PATH = "data/Plate1.csv";
                        this.plate = GenerateExample(PATH);
                        using(var writer = new StreamWriter("data/AcutalPlate.csv"))
                        {
                            for(int i=0; i<8; i++){
                                for(int j=0; j<8; j++){
                                    writer.Write(plate[i,j]);
                                    if(j!=7){
                                        writer.Write(",");
                                    }
                                }
                                writer.WriteLine();
                            }
                        }
                    break;
                    case 1:
                        PATH = "data/Lettre.txt";
                        this.plate = GenerateRandomPlate(PATH);
                        using(var writer = new StreamWriter("data/AcutalPlate.csv"))
                        {
                            for(int i=0; i<8; i++){
                                for(int j=0; j<8; j++){
                                    writer.Write(plate[i,j]);
                                    if(j!=7){
                                        writer.Write(",");
                                    }
                                }
                                writer.WriteLine();
                            }
                        }
                    break;


                }
            break;

            case -1:
            case -2:
                Console.WriteLine("Vous avez pressé la touche echap, vous allez sortir du jeu, pressez entrée pour confirmer");
                Console.ReadKey();
                Core.ExitProgram();
            break;
        }
        path = "data/AcutalPlate.csv";
    }



    public string PATH {get {return path;} set {path = value;}}
    public char[,] Plate{get {return plate;} set {plate = value;}}



    public char[,] GenerateExample(string PATH){
        char[,] plate = new char[8,8];
        string[] lines = File.ReadAllLines(PATH);
        if(lines is not null){
            for(int i=0; i<8; i++){
                string[] parts = lines[i].Split(',');
                for(int j=0; j<8; j++){
                    plate[i,j] = char.Parse(parts[j]);
                }
            }
        }
        else{
            Console.WriteLine("Le fichier n'existe pas");
        }
        return plate;
    }

    public char[,] GenerateRandomPlate(string PATH){
        Random r = new Random();
        Dictionary<char,int> dico = new Dictionary<char,int>();

        using(StreamReader reader = new StreamReader(PATH)){
            string line = reader.ReadLine() ?? "";
            while(line != null){
                string[] parts = line.Split(',');
                dico.Add(char.Parse(parts[0]), int.Parse(parts[1]));
                line = reader.ReadLine();
            }
        }

        char[,] plate = new char[8,8];
        for(int i=0; i<8; i++){
            for(int j=0; j<8; j++){
                // Operation to generate a random letter
                var keys = dico.Keys.ToList();
                int rand = r.Next(0, keys.Count);
                char key = keys[rand];

                if(dico[key] !=0){
                    plate[i,j] = keys[rand];
                    dico[key] -= 1;
                }
                else{
                    j--;
                }
            }
        }
        return plate;

        
    }
    
    public string toString()
    {
        string plate_string="";
        using(var reader = new StreamReader("data/AcutalPlate.csv"))
        {
            string line;
            while((line = reader.ReadLine()) != null){
                string[] parts = line.Split(',');
                for(int i=0; i<8; i++){
                    if(parts[i]!=null){
                        plate_string+="| "+parts[i]+" |";
                    }
                }
                plate_string+="\n"; 
            }
        }
        return plate_string;
    }

    public void Read(string path)
{
    string[] lines = File.ReadAllLines(path); 
    if(lines is not null){
        for(int i=0; i<8; i++){
            string[] parts = lines[i].Split(',');
            for(int j=0; j<8; j++){
                plate[i,j] = char.Parse(parts[j]);
            }
        }
    }
    else{
        Console.WriteLine("Le fichier n'existe pas");
    }
}

    public void ToFile(string nomfile) 
    {
        using(StreamWriter reader = new StreamWriter(nomfile)){
            for(int i=0; i<8; i++){
                for(int j=0; j<8; j++){
                    reader.Write(plate[i,j]);
                    if(j!=7){
                        reader.Write(",");
                    }
                    Console.WriteLine(plate[i,j]);
                }
                reader.WriteLine();
            }
        }
    }

    public bool Recherche_Mot(int x, int y, string word, int compteur = 0 )
    {
        if(compteur == 0){
            return true;
        }
        if(x == 0 || y==0 || x==8 || y==8){
            return false;
        }
        char character = plate[x,y];
        bool way = Recherche_Mot(x+1,y,word,compteur++) || Recherche_Mot(x-1,y,word,compteur++) || Recherche_Mot(x,y+1,word,compteur++);
        plate[x,y] = ' ';
        if(!way){
            plate[x,y] = character;
        }
        return way; 
    }
}



/*





public void Maj_Plateau(object objet)
{
    
}

}
*/
