namespace Projet_A2_S1
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            Method.main_menu(); // create the main menu
            Core.ClearWindow();
            Dictionnaire dico = new Dictionnaire();
            Console.WriteLine("coucou");
            Console.WriteLine(dico.toString());
            Console.WriteLine(dico.FindWord("BUCHEUR"));
        }
    }
}

