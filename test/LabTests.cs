using System.IO;
using NUnit.Framework;
using CSharpBrainTeasers;

namespace CSharpBrainTeasers.Tests;

[TestFixture]
public class LabTests
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

    [Test]
    public void Run_VoidMethod_ExecutesSuccessfully()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            Lab.Run(() => TestVoidMethod(5, "test"));
        });

        // Assert
        Assert.That(output, Does.Contain("=== Execution Analysis ==="));
        Assert.That(output, Does.Contain("LabTests.TestVoidMethod(Int32 number = 5, String text = \"test\")"));
        Assert.That(output, Does.Contain("✅ Execution completed successfully!"));
        Assert.That(output, Does.Contain("📋 Result: void"));
        Assert.That(output, Does.Contain("Method executed with: 5, test"));
    }

    [Test]
    public void Run_FunctionWithReturnValue_ReturnsCorrectResult()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestAddNumbers(10, 20));
            Assert.That(result, Is.EqualTo(30));
        });

        // Assert
        Assert.That(output, Does.Contain("=== Execution Analysis ==="));
        Assert.That(output, Does.Contain("LabTests.TestAddNumbers(Int32 a = 10, Int32 b = 20)"));
        Assert.That(output, Does.Contain("✅ Execution completed successfully!"));
        Assert.That(output, Does.Contain("📋 Result: 30(Int32)"));
    }

    [Test]
    public void Run_FunctionWithStringReturn_FormatsStringCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestStringConcat("Hello", "World"));
            Assert.That(result, Is.EqualTo("Hello World"));
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestStringConcat(String first = \"Hello\", String second = \"World\")"));
        Assert.That(output, Does.Contain("📋 Result: Hello World(String)"));
    }

    [Test]
    public void Run_FunctionWithArrayReturn_FormatsArrayCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestCreateArray(3));
            Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestCreateArray(Int32 size = 3)"));
        Assert.That(output, Does.Contain("📋 Result: [1, 2, 3] (Length: 3)(Int32[])"));
    }

    [Test]
    public void Run_FunctionWithListReturn_FormatsListCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestCreateList("a", "b", "c"));
            Assert.That(result, Is.EqualTo(new List<string> { "a", "b", "c" }));
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestCreateList(String item1 = \"a\", String item2 = \"b\", String item3 = \"c\")"));
        Assert.That(output, Does.Contain("📋 Result: [a, b, c](List`1)"));
    }

    [Test]
    public void Run_FunctionWithNullReturn_HandlesNullCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestReturnNull());
            Assert.That(result, Is.Null);
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestReturnNull()"));
        Assert.That(output, Does.Contain("📋 Result: null(String)"));
    }

    [Test]
    public void Run_FunctionThatThrowsException_CatchesAndReportsException()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestThrowException("Test error"));
            Assert.That(result, Is.EqualTo(0)); // Should return default value
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestThrowException(String message = \"Test error\")"));
        Assert.That(output, Does.Contain("❌ Exception occurred during execution!"));
        Assert.That(output, Does.Contain("🚨 Exception: Test error"));
        Assert.That(output, Does.Contain("ArgumentException"));
        Assert.That(output, Does.Contain("📍 Stack trace"));
    }

    [Test]
    public void Run_FunctionWithComplexParameters_FormatsParametersCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            var result = Lab.Run(() => TestComplexParameters(42, 'A', true, null));
            Assert.That(result, Is.EqualTo("42-A-True-null"));
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestComplexParameters(Int32 number = 42, Char letter = 'A', Boolean flag = True, object nullValue = null)"));
        Assert.That(output, Does.Contain("📋 Result: 42-A-True-null(String)"));
    }

    [Test]
    public void Run_VoidMethodWithNoParameters_ExecutesCorrectly()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            Lab.Run(() => TestNoParameters());
        });

        // Assert
        Assert.That(output, Does.Contain("LabTests.TestNoParameters()"));
        Assert.That(output, Does.Contain("✅ Execution completed successfully!"));
        Assert.That(output, Does.Contain("📋 Result: void"));
        Assert.That(output, Does.Contain("No parameters method executed"));
    }

    [Test]
    public void Run_TimingMeasurement_IncludesExecutionTime()
    {
        // Arrange & Act
        var output = CaptureConsoleOutput(() =>
        {
            Lab.Run(() => TestWithDelay());
        });

        // Assert
        Assert.That(output, Does.Contain("⏱️  Execution time:"));
        Assert.That(output, Does.Contain("ms"));
        // Should be at least 100ms due to the delay
        var timingLine = output.Split('\n').FirstOrDefault(line => line.Contains("Execution time:"));
        Assert.That(timingLine, Is.Not.Null);
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
