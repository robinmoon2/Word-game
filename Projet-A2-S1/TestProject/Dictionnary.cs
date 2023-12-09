namespace TestProject{
    public class Dictionary{
        

        [Test]
        [TestCase("bonjour",true)] // true
        [TestCase("au-revoir",false)] // false
        [TestCase("",false)] // false
        [TestCase("TeSt",false)] // true
        [TestCase("coucou",true)] // true
        [TestCase("329843ZFJEZ+2%R3ZP3",false)] // false
        public void TestFindWord(string word, bool result){
            var dictionary = new CustomDictionary();
            bool verif = dictionary.FindWord(word);
            Assert.That(result, Is.EqualTo(verif));
        }

    }
}