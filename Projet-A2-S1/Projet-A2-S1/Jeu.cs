using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppVisuals;

namespace Projet_A2_S1
{
    internal class Jeu
    {

        static public void beginning(){
            Core.ClearWindow();
            var index = Core.ScrollingNumberSelector("Choisir un nombre de joueur :",1, 4,1,1);
            for(int i=0; i<index.Item2;i++){
                Console.WriteLine("Entrez le nom du joueur :");
                string nom = Console.ReadLine();
                ConsoleColor color;
                if(i==0){
                    Console.WriteLine("coucou");
                     color = ConsoleColor.Blue;
                }
                else if(i==1){
                     color = ConsoleColor.Green;
                }
                else{
                     color = ConsoleColor.Red;
                }
                Joueur coucou = new Joueur(nom,120,color);
                Core.ChangeForeground(color);
                string s = coucou.toString();
                Core.WritePositionedString(s,Placement.Right,default,2*i+9,default);
                Console.WriteLine();
                Console.WriteLine();
                
            }
        }
    }
}
