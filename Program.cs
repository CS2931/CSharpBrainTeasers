using System;
using CSharpBrainTeasers.BrainTeasers;

namespace CSharpBrainTeasers;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Brain Teasers Collection ===");
        Console.WriteLine();

        Lab.Run(() => ExampleDivideNumbers(10, 2));
    }
    
    static void ExampleDivideNumbers(int a, int b)
    {
        Console.WriteLine("Brain Teaser #1: Divide Numbers");
        try
        {
            double result = a / b;
            Console.WriteLine($"Solution: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

