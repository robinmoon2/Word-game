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
            if (player.Contient(word) && player.WordList.Count() !=0)
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
            if (player.WordList.Contains(word))
                verif = true;
            Assert.That(player.Contient(word), Is.EqualTo(verif));
        }

        [Test]
        [TestCase("coucou")]
        [TestCase("bonjour")]
        [TestCase("1234566")]
        [TestCase("")]
        public void TestAddScore(string word)
        {
            Player player = new Player { Name = "joueur", Timer = 60, Score = 0, WordList = new List<string>() };
            player.Add_Score(player.Word_Value(word));
            Assert.That(player.Word_Value(word), Is.EqualTo(player.Score));
        }
    }
}