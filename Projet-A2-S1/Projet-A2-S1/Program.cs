using ConsoleAppVisuals;
namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Core.ClearWindow();
            Core.WriteBanner();
            (int a , int b)= Core.ScrollingMenuSelector(" Menu Principal :", default, default, "Jouer", "Paramètre", "Quitter le jeu");
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}