//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

//public class MyDictionary<TKey, UValue> : IEnumerable<KeyValuePair<TKey, UValue>>
//{
//    private List<KeyValuePair<TKey, UValue>> _values = new List<KeyValuePair<TKey, UValue>>();

//    public int Count => _values.Count;

//    public MyDictionary()
//    {
//    }

//    public void Add(TKey key, UValue @value)
//    {
//        var exists = false;
//        for (var i = 0; i < _values.Count; i++)
//        {
//            if (_values[i].Key.Equals(key))
//                exists = true;
//        }
//        if (!exists)
//            _values.Add(new KeyValuePair<TKey, UValue>(key, @value));
//        else
//            throw new Exception($"key {key} already present");
//    }

//    public bool ContainsKey(TKey key)
//    {
//        var exists = false;
//        for (var i = 0; i < _values.Count; i++)
//        {
//            if (_values[i].Key.Equals(key))
//                exists = true;
//        }
//        return exists;
//    }

//    public UValue this[TKey key]
//    {
//        get
//        {
//            UValue result = default(UValue);
//            for (var i = 0; i < _values.Count; i++)
//            {
//                if (_values[i].Key.Equals(key))
//                    result = _values[i].Value;
//            }
//            if (!result.Equals(default(UValue)))
//                return result;
//            else
//                throw new Exception($"key {key} is not present");
//        }
//        set
//        {
//            var exists = false;
//            for (var i = 0; i < _values.Count; i++)
//            {
//                if (_values[i].Key.Equals(key))
//                {
//                    _values[i] = new KeyValuePair<TKey, UValue>(key, value);
//                    exists = true;
//                }
//            }
//            if (!exists)
//                _values.Add(new KeyValuePair<TKey, UValue>(key, value));
//        }
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }

//    public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
//    {
//        return _values.GetEnumerator();
//    }
//}

//public class Program
//{
//    public static void Main()
//    {
//        var books = new MyDictionary<string, Book>();

//        //Add
//        Console.WriteLine("[TEST] Add books");
//        books.Add("Joe", new Book("Essential C# 8.0", 2023));
//        books.Add("Jane", new Book("Visual C Sharp Step by Step", 2018));
//        books.Add("Mindy", new Book("Professional C 2012 and .NET 4.5", 2014));
//        foreach (var book in books)
//            Console.WriteLine(book.ToString());
//        Console.WriteLine("");

//        //Add existing
//        Console.WriteLine("[TEST] Add existing book");
//        var exceptionThrown = false;
//        try
//        {
//            books.Add("Joe", new Book("Essential C# 8.0", 2023));
//        }
//        catch (Exception)
//        {
//            exceptionThrown = true;
//        }
//        if (exceptionThrown)
//            Console.WriteLine("Exception was thrown");
//        else
//            throw new Exception("Exception was not thrown");
//        Console.WriteLine("");

//        //Looking for item
//        Console.WriteLine("[TEST] Looking for Joe book");
//        var book1 = books["Joe"];
//        Console.WriteLine(book1.ToString());
//        Console.WriteLine("");

//        //Contains key
//        Console.WriteLine("[TEST] Contains Jane book");
//        Console.WriteLine(books.ContainsKey("Jane"));
//        Console.WriteLine("");

//        //Replace item
//        Console.WriteLine("[TEST] Replace Mindy book");
//        var book2 = books["Mindy"];
//        Console.WriteLine($"Current book: {book2}");
//        books["Mindy"] = new Book("How to Code like a God", 2025);
//        book2 = books["Mindy"];
//        Console.WriteLine($"New  book: {book2}");
//        Console.WriteLine("");


//        // Perf
//        Console.WriteLine("[TEST] Performance");
//        PerfTest(10000);
//        Console.WriteLine("");
//        PerfTest(100000);
//        Console.WriteLine("");
//        PerfTest(1000000);
//        Console.WriteLine("");
//    }

//    private static readonly Stopwatch Stopwatch = new Stopwatch();

//    private static void PerfTest(int nbrOfIteration)
//    {
//        var msDico = new Dictionary<string, string>();
//        var myDico = new MyDictionary<string, string>();
//        var msTime = TimeSpan.Zero;
//        var myTime = TimeSpan.Zero;

//        Console.WriteLine($"Number of Iteration: {nbrOfIteration}");
//        Console.WriteLine("                   |        MS        |        MY        ");

//        Stopwatch.Start();
//        for (var i = 0; i < nbrOfIteration; ++i)
//        {
//            var id = Guid.NewGuid().ToString();
//            msDico.Add(id, id);
//        }
//        Stopwatch.Stop();
//        msTime = Stopwatch.Elapsed;
//        Stopwatch.Reset();
//        Stopwatch.Start();
//        for (var i = 0; i < nbrOfIteration; ++i)
//        {
//            var id = Guid.NewGuid().ToString();
//            myDico.Add(id, id);
//        }
//        Stopwatch.Stop();
//        myTime = Stopwatch.Elapsed;
//        Stopwatch.Reset();

//        Console.WriteLine($"Time to add items  | {msTime} | {myTime}");

//        Stopwatch.Start();
//        foreach (var idKey in msDico)
//        {
//            var idVal = msDico[idKey.Key];
//            if (idVal != idKey.Key)
//                throw new Exception("val != key");
//        }
//        Stopwatch.Stop();
//        msTime = Stopwatch.Elapsed;
//        Stopwatch.Reset();
//        Stopwatch.Start();
//        foreach (var idKey in myDico)
//        {
//            var idVal = myDico[idKey.Key];
//            if (idVal != idKey.Key)
//                throw new Exception("val != key");
//        }
//        Stopwatch.Stop();
//        myTime = Stopwatch.Elapsed;
//        Stopwatch.Reset();

//        Console.WriteLine($"Time to read items | {msTime} | {myTime}");
//    }
//}

//public class Book
//{
//    public string Name { get; set; }
//    public int Year { get; set; }

//    public Book(string Name, int Year)
//    {
//        this.Name = Name;
//        this.Year = Year;
//    }

//    public override string ToString()
//    {
//        return $"{Name} ({Year})";
//    }
//}
