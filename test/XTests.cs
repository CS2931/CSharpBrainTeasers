using System.IO;
using Xunit;
using CSharpBrainTeasers;

namespace CSharpBrainTeasers.Tests;

public class XTests
{
    // Helper method to capture console output
    private string CaptureConsoleOutput(Action action)
    {
        var originalOut = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        try
        {
            action();
            return stringWriter.ToString();
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void Run_VoidMethod_ExecutesSuccessfully()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            X.Run(() => TestVoidMethod(5, "test"));
        });

        // Assert
        Assert.Contains("=== Execution Analysis ===", output);
        Assert.Contains("XTests.TestVoidMethod(Int32 number = 5, String text = \"test\")", output);
        Assert.Contains("✅ Execution completed successfully!", output);
        Assert.Contains("📋 Result: void", output);
        Assert.Contains("Method executed with: 5, test", output);
    }

    [Fact]
    public void Run_FunctionWithReturnValue_ReturnsCorrectResult()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestAddNumbers(10, 20));
            Assert.Equal(30, result);
        });

        // Assert
        Assert.Contains("=== Execution Analysis ===", output);
        Assert.Contains("XTests.TestAddNumbers(Int32 a = 10, Int32 b = 20)", output);
        Assert.Contains("✅ Execution completed successfully!", output);
        Assert.Contains("📋 Result: 30(Int32)", output);
    }

    [Fact]
    public void Run_FunctionWithStringReturn_FormatsStringCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestStringConcat("Hello", "World"));
            Assert.Equal("Hello World", result);
        });

        // Assert
        Assert.Contains("XTests.TestStringConcat(String first = \"Hello\", String second = \"World\")", output);
        Assert.Contains("📋 Result: Hello World(String)", output);
    }

    [Fact]
    public void Run_FunctionWithArrayReturn_FormatsArrayCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestCreateArray(3));
            Assert.Equal([1, 2, 3], result);
        });

        // Assert
        Assert.Contains("XTests.TestCreateArray(Int32 size = 3)", output);
        Assert.Contains("📋 Result: [1, 2, 3] (Length: 3)(Int32[])", output);
    }

    [Fact]
    public void Run_FunctionWithListReturn_FormatsListCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestCreateList("a", "b", "c"));
            Assert.Equal(["a", "b", "c"], result);
        });

        // Assert
        Assert.Contains("XTests.TestCreateList(String item1 = \"a\", String item2 = \"b\", String item3 = \"c\")", output);
        Assert.Contains("📋 Result: [a, b, c](List`1)", output);
    }

    [Fact]
    public void Run_FunctionWithNullReturn_HandlesNullCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestReturnNull());
            Assert.Null(result);
        });

        // Assert
        Assert.Contains("XTests.TestReturnNull()", output);
        Assert.Contains("📋 Result: null(String)", output);
    }

    [Fact]
    public void Run_FunctionThatThrowsException_CatchesAndReportsException()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestThrowException("Test error"));
            Assert.Equal(0, result); // Should return default value
        });

        // Assert
        Assert.Contains("XTests.TestThrowException(String message = \"Test error\")", output);
        Assert.Contains("❌ Exception occurred during execution!", output);
        Assert.Contains("🚨 Exception: Test error", output);
        Assert.Contains("ArgumentException", output);
        Assert.Contains("📍 Stack trace", output);
    }

    [Fact]
    public void Run_FunctionWithComplexParameters_FormatsParametersCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = X.Run(() => TestComplexParameters(42, 'A', true, null));
            Assert.Equal("42-A-True-null", result);
        });

        // Assert
        Assert.Contains("XTests.TestComplexParameters(Int32 number = 42, Char letter = 'A', Boolean flag = True, object nullValue = null)", output);
        Assert.Contains("📋 Result: 42-A-True-null(String)", output);
    }

    [Fact]
    public void Run_VoidMethodWithNoParameters_ExecutesCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            X.Run(() => TestNoParameters());
        });

        // Assert
        Assert.Contains("XTests.TestNoParameters()", output);
        Assert.Contains("✅ Execution completed successfully!", output);
        Assert.Contains("📋 Result: void", output);
        Assert.Contains("No parameters method executed", output);
    }

    [Fact]
    public void Run_TimingMeasurement_IncludesExecutionTime()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            X.Run(() => TestWithDelay());
        });

        // Assert
        Assert.Contains("⏱️  Execution time:", output);
        Assert.Contains("ms", output);
        // Should be at least 100ms due to the delay
        var timingLine = output.Split('\n').FirstOrDefault(line => line.Contains("Execution time:"));
        Assert.NotNull(timingLine);
    }

    // Test helper methods
    private void TestVoidMethod(int number, string text)
    {
        Console.WriteLine($"Method executed with: {number}, {text}");
    }

    private int TestAddNumbers(int a, int b)
    {
        return a + b;
    }

    private string TestStringConcat(string first, string second)
    {
        return $"{first} {second}";
    }

    private int[] TestCreateArray(int size)
    {
        return Enumerable.Range(1, size).ToArray();
    }

    private List<string> TestCreateList(string item1, string item2, string item3)
    {
        return [item1, item2, item3];
    }

    private string? TestReturnNull()
    {
        return null;
    }

    private int TestThrowException(string message)
    {
        throw new ArgumentException(message);
    }

    private string TestComplexParameters(int number, char letter, bool flag, string? nullValue)
    {
        return $"{number}-{letter}-{flag}-{nullValue ?? "null"}";
    }

    private void TestNoParameters()
    {
        Console.WriteLine("No parameters method executed");
    }

    private void TestWithDelay()
    {
        Thread.Sleep(100); // Add a small delay to test timing
        Console.WriteLine("Delayed method executed");
    }
}
