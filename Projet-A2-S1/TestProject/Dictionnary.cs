namespace TestProject{
    public class Dictionary{
        

        [Test]
        [TestCase("bonjour",true)] 
        [TestCase("au-revoir",false)] 
        [TestCase("",false)] 
        [TestCase("TeSt",true)] 
        [TestCase("coucou",true)] 
        [TestCase("329843ZFJEZ+2%R3ZP3",false)] 
        public void TestFindWord(string word, bool result){
            var dictionary = new CustomDictionary();
            bool verif = dictionary.FindWord(word);
            Assert.That(result, Is.EqualTo(verif));
        }
    }
}