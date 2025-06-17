namespace Code_tests;

[TestClass]
public class IEnumerableTests
{
    [TestMethod]
    public void IEnumerableTest()
    {
        IEnumerable<char> list = new List<char>() { 'a', 'b', 'z' };
        list = "Toto";
        list=new char[] { 'a', 'b', 'z' };

        // IEnumerable impose une fonction GetEnumerator
        var enumerator=list.GetEnumerator();
        while (enumerator.MoveNext()) { 
            var e=enumerator.Current;
            Console.WriteLine(e);   
        }

        foreach(var e in list)
        {
            Console.WriteLine(e);
        }

        var listeChar = "aczfs".OrderBy(c => c); //  acfsz

        var entiers = new List<int>() { 5, 9, 1, 3, 6 };

        // Je ne cr�� pas d'objet en m�m�oire avec les r�sultats du filtre
        // Je cr�� uniquement une "Vue" correspondant au filtrage
        // Pas de RAM mais refiltrage � chaque �numation
        var petitsEntiers = entiers.Where(c => 
                    c < 6); // 5,1,3

        // je filtre et je stocke dans une liste
        // Prend de la m�moire mais le filtrage est effectu� de mani�ere d�finitive
        var petitsEntiersMat�rialis�s = entiers.Where(c =>
            c < 6).ToList(); // 5,1,3

        var c = petitsEntiers.Count(); // 3

        entiers.Add(4);
         c = petitsEntiers.Count(); // 4
        c = petitsEntiersMat�rialis�s.Count(); // 3 
    }
}
