using System.Collections;
using System.Runtime.ExceptionServices;
using YamlDotNet.Serialization.BufferedDeserialization.TypeDiscriminators;

namespace Projet_A2_S1;

public class Plateau
{
    string path;



    public Plateau(string PATH="")
    {
        var difficulty_idex = Core.ScrollingMenuSelector("Choisissez un plateau",default,default, "Plateau par défaut", "Plateau personnalisé" );
        switch(difficulty_idex.Item1){
            case 0:
                switch(difficulty_idex.Item2){
                    case 0:
                        PATH = "data/Plate1.csv";
                        using(var reader = new StreamReader(PATH))
                        {
                            File.Copy("data/Plate1.csv", "data/AcutalPlate.csv", true);
                        }
                    break;
                    case 1:
                        PATH = "data/Lettre.txt";
                        char[,] plate = GeneratePlate(PATH);
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
    }

    public string PATH {get {return path;} set {path = value;}}
    

    public char[,] GeneratePlate(string PATH){
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
}

/*
public string toString()
{
    
}
public void ToFile(string nomfile) 
{
    
}

public void ToRead(string nomfile)

{
    
}
public object Recherche_Mot(string mot)
{

}


public void Maj_Plateau(object objet)
{
    
}

}
*/
