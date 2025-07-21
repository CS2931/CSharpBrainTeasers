namespace CSharpBrainTeasers.BrainTeasers;

public static class ExampleTeasers
{
    public static void FibonacciSequence()
    {
        Console.WriteLine("Math Problem: Fibonacci Sequence");

        int n = 10;
        Console.WriteLine($"First {n} Fibonacci numbers:");

        for (int i = 0; i < n; i++)
        {
            Console.Write($"{Fibonacci(i)} ");
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    private static int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public static void PrimeChecker()
    {
        Console.WriteLine("Math Problem: Prime Number Checker");

        int[] testNumbers = { 2, 3, 4, 17, 25, 29, 100 };

        foreach (int num in testNumbers)
        {
            bool isPrime = IsPrime(num);
            Console.WriteLine($"{num} is {(isPrime ? "prime" : "not prime")}");
        }

        Console.WriteLine();
    }

    private static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}

