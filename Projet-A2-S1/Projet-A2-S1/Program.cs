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
            Core.ScrollingMenuSelector(question : "C'est beau hein ?", defaultIndex : default, line : default,"Option 1", "Option 2", "Option 3");
            
        }
    }
}