using System;
using CSharpBrainTeasers.BrainTeasers;

namespace CSharpBrainTeasers;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Brain Teasers Collection ===");
        Console.WriteLine();
        
        // Run brain teasers here from the brain teasers namespace
        // ExampleTeasers.FibonacciSequence();
        // ExampleTeasers.PrimeChecker();

        // Alternatively, run static methods within programme.cs here
        Example1();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    static void Example1()
    {
        Console.WriteLine("Brain Teaser #1: Your Custom Problem");
        Console.WriteLine("Solution: ...");
        Console.WriteLine();
    }
}

