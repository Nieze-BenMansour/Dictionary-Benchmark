using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        IMyDictionary<string, Book> booksMyArrayDictionary = new MyArrayDictionary<string, Book>();
        IMyDictionary<string, Book> booksNotOptimized = new MyDictionaryNotOptimized<string, Book>();
        IMyDictionary<string, Book> booksMyHashSetDictionary = new MyHashSetDictionary<string, Book>();
        IMyDictionary<string, Book> booksMyLinkedListDictionary = new MyLinkedListDictionary<string, Book>();
        IMyDictionary<string, Book> booksMySpanDictionary = new MySpanDictionary<string, Book>();

        Console.WriteLine(" ****** [TEST booksMyArrayDictionary] Add existing book");
        TestCaseForBooks(booksMyArrayDictionary);
        Console.WriteLine(" ****** [TEST booksNotOptimized] Add existing book");
        TestCaseForBooks(booksNotOptimized);
        Console.WriteLine(" ****** [TEST booksMyHashSetDictionary] Add existing book");
        TestCaseForBooks(booksMyHashSetDictionary);
        Console.WriteLine(" ****** [TEST booksMyLinkedListDictionary] Add existing book");
        TestCaseForBooks(booksMyLinkedListDictionary);
        Console.WriteLine(" ****** [TEST booksMySpanDictionary] Add existing book");
        TestCaseForBooks(booksMySpanDictionary);
    }

    private static void TestCaseForBooks(IMyDictionary<string, Book> booksDictionary)
    {
        //Add
        Console.WriteLine("[TEST] Add books");
        booksDictionary.Add("Joe", new Book("Essential C# 8.0", 2023));
        booksDictionary.Add("Jane", new Book("Visual C Sharp Step by Step", 2018));
        booksDictionary.Add("Mindy", new Book("Professional C 2012 and .NET 4.5", 2014));
        foreach (var book in booksDictionary)
            Console.WriteLine(book.ToString());
        Console.WriteLine("");

        //Add existing
        Console.WriteLine("[TEST] Add existing book");
        var exceptionThrown = false;
        try
        {
            booksDictionary.Add("Joe", new Book("Essential C# 8.0", 2023));
        }
        catch (Exception)
        {
            exceptionThrown = true;
        }
        if (exceptionThrown)
            Console.WriteLine("Exception was thrown");
        else
            throw new Exception("Exception was not thrown");
        Console.WriteLine("");

        //Looking for item
        Console.WriteLine("[TEST] Looking for Joe book");
        var book1 = booksDictionary["Joe"];
        Console.WriteLine(book1.ToString());
        Console.WriteLine("");

        //Contains key
        Console.WriteLine("[TEST] Contains Jane book");
        Console.WriteLine(booksDictionary.ContainsKey("Jane"));
        Console.WriteLine("");

        //Replace item
        Console.WriteLine("[TEST] Replace Mindy book");
        var book2 = booksDictionary["Mindy"];
        Console.WriteLine($"Current book: {book2}");
        booksDictionary["Mindy"] = new Book("How to Code like a God", 2025);
        book2 = booksDictionary["Mindy"];
        Console.WriteLine($"New  book: {book2}");
        Console.WriteLine("");


        // Perf
        Console.WriteLine("[TEST] Performance");
        PerfTest(1000);
        Console.WriteLine("");
        PerfTest(10000);
        Console.WriteLine("");
        PerfTest(100000);
        Console.WriteLine("");
    }

    private static readonly Stopwatch Stopwatch = new Stopwatch();

    private static void PerfTest(int nbrOfIteration)
    {
        var msDico = new Dictionary<string, string>();
        var myDico = new MyArrayDictionary<string, string>();
        var msTime = TimeSpan.Zero;
        var myTime = TimeSpan.Zero;

        Console.WriteLine($"Number of Iteration: {nbrOfIteration}");
        Console.WriteLine("                   |        MS        |        MY        ");

        Stopwatch.Start();
        for (var i = 0; i < nbrOfIteration; ++i)
        {
            var id = Guid.NewGuid().ToString();
            msDico.Add(id, id);
        }
        Stopwatch.Stop();
        msTime = Stopwatch.Elapsed;
        Stopwatch.Reset();
        Stopwatch.Start();
        for (var i = 0; i < nbrOfIteration; ++i)
        {
            var id = Guid.NewGuid().ToString();
            myDico.Add(id, id);
        }
        Stopwatch.Stop();
        myTime = Stopwatch.Elapsed;
        Stopwatch.Reset();

        Console.WriteLine($"Time to add items  | {msTime} | {myTime}");

        Stopwatch.Start();
        foreach (var idKey in msDico)
        {
            var idVal = msDico[idKey.Key];
            if (idVal != idKey.Key)
                throw new Exception("val != key");
        }
        Stopwatch.Stop();
        msTime = Stopwatch.Elapsed;
        Stopwatch.Reset();
        Stopwatch.Start();
        foreach (var idKey in myDico)
        {
            var idVal = myDico[idKey.Key];
            if (idVal != idKey.Key)
                throw new Exception("val != key");
        }
        Stopwatch.Stop();
        myTime = Stopwatch.Elapsed;
        Stopwatch.Reset();

        Console.WriteLine($"Time to read items | {msTime} | {myTime}");
    }
}
