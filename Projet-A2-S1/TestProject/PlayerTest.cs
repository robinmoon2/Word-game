using NuGet.Frameworks;
using NUnit.Framework.Interfaces;
using System.Security;

namespace TestProject
{
    public class PlayerTest
    {

        [Test]
        [TestCase("coucou")]
        [TestCase("")]
        [TestCase("12345")]
        public void WordListEmpty(string word)
        {
            Player player = new Player { Name = "joueur", Timer = 60, Score = 0, WordList = new List<string>() };
            player.Add_Mot(word);
            bool verif = false;
            if (player.WordList.Contains(word) && player.WordList.Count() == 1)
                verif = true;
            Assert.That(true, Is.EqualTo(verif));
        }

        [Test]
        [TestCase("coucou")]
        [TestCase("bonjour")]
        [TestCase("")]
        [TestCase("12345")]
        public void WordList(string word)
        {
            Player player = new Player { Name = "joueur", Timer = 60, Score = 0, WordList = new List<string>() };
            player.WordList.Add("bonjour");
            bool verif = false;
            if (player.WordList.Contains(word) && player.WordList.Count() == 2)
                verif = true;
            Assert.That(true, Is.EqualTo(verif));

        }

        [Test]
        [TestCase("coucou")]
        [TestCase("bonjour")]
        [TestCase("")]
        [TestCase("12345")]
        public void TestAddScore(string word)
        {
            Player player = new Player { Name = "joueur", Timer = 60, Score = 0, WordList = new List<string>() };
            player.Add_Mot(word);
            player.Add_Score(player.Word_Value(word));
            bool verif = false;
            if (player.Score != 0 )
                verif = true;
            Assert.That(true, Is.EqualTo(verif));
        }


    }
}