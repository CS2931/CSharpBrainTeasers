# CSharpBrainTeasers.Tests

This directory contains comprehensive unit tests for the C# Brain Teasers project, specifically testing the **Lab.Run()** execution analysis framework using **NUnit 4.0.1**.

## 📁 Project Structure

```
test/
├── XTests.cs                           # Main test suite for Lab.Run() framework
├── GlobalUsings.cs                     # Global using statements
├── CSharpBrainTeasers.Tests.csproj     # Test project file with NUnit packages
├── packages.config                     # NuGet package configuration
├── runsettings.xml                     # Test execution settings
└── README.md                           # This file
```

## 🧪 Test Framework: NUnit 4.0.1

We use **NUnit 4.0.1** (latest version) for our testing framework, which provides:

- ✅ **Fluent Assertions**: `Assert.That()` with readable syntax
- ✅ **Rich Constraint Model**: `Does.Contain()`, `Is.EqualTo()`, `Is.Null`, etc.
- ✅ **Test Discovery**: Automatic test detection in VS Code
- ✅ **Parallel Execution**: Fast test runs
- ✅ **Detailed Reporting**: Multiple output formats (console, TRX, HTML)

## 🎯 Test Coverage

### **Lab.Run() Framework Tests (10 Tests)**

Our test suite comprehensively validates the Lab.Run() execution analysis framework:

#### **Core Functionality Tests**
- ✅ **Void Method Execution** - Tests methods with no return value
- ✅ **Function Return Values** - Tests methods that return values
- ✅ **Parameter Detection** - Validates automatic parameter extraction
- ✅ **Method Signature Formatting** - Ensures correct display format

#### **Data Type Handling Tests**
- ✅ **String Formatting** - Tests string parameter and return formatting
- ✅ **Array Formatting** - Tests array return value display with length
- ✅ **List/Collection Formatting** - Tests generic collection handling
- ✅ **Null Value Handling** - Tests null parameter and return values
- ✅ **Complex Parameters** - Tests multiple parameter types (int, char, bool, null)

#### **Advanced Feature Tests**
- ✅ **Exception Handling** - Tests error catching and stack trace reporting
- ✅ **Execution Timing** - Validates performance measurement accuracy

## 🔧 Package Dependencies

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
<PackageReference Include="NUnit" Version="4.0.1" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
<PackageReference Include="NUnit.Analyzers" Version="3.9.0" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

## 🚀 Running Tests

### **Command Line Options**

```bash
# Navigate to test directory
cd test

# Basic test run
dotnet test

# Verbose output with detailed results
dotnet test --logger "console;verbosity=normal"

# Run with multiple loggers (console + TRX + HTML)
dotnet test --logger trx --logger "console;verbosity=detailed"

# Run specific test by name
dotnet test --filter "Run_VoidMethod_ExecutesSuccessfully"

# Run all tests in XTests class
dotnet test --filter "XTests"

# Run tests matching a pattern
dotnet test --filter "Run_Function*"

# Watch mode - automatically re-run tests when files change
dotnet test --watch

# Run tests from solution root
dotnet test test/CSharpBrainTeasers.Tests.csproj
```

### **VS Code Integration**

#### **Test Explorer Panel**
- View all tests in a tree structure
- Click to run individual tests or test groups
- View test results with pass/fail indicators
- Access test output and error details

#### **CodeLens Integration**
- **Run Test** and **Debug Test** buttons appear above each `[Test]` method
- Click to execute individual tests directly from the editor
- Set breakpoints and debug tests step-by-step

#### **Command Palette Commands**
- `Ctrl+Shift+P` → **"Test: Run All Tests"**
- `Ctrl+Shift+P` → **"Test: Debug All Tests"**
- `Ctrl+Shift+P` → **"Test: Run Tests in Current File"**
- `Ctrl+Shift+P` → **"Tasks: Run Task"** → **"test"**
- `Ctrl+Shift+P` → **"Tasks: Run Task"** → **"test-watch"**

## 📊 Test Output Examples

### **Successful Test Output**
```
NUnit Adapter 4.5.0.0: Test execution started
Running all tests in CSharpBrainTeasers.Tests.dll

✅ Passed Run_VoidMethod_ExecutesSuccessfully [2 ms]
✅ Passed Run_FunctionWithReturnValue_ReturnsCorrectResult [1 ms]
✅ Passed Run_FunctionWithStringReturn_FormatsStringCorrectly [1 ms]
✅ Passed Run_FunctionWithArrayReturn_FormatsArrayCorrectly [21 ms]
✅ Passed Run_FunctionWithListReturn_FormatsListCorrectly [5 ms]
✅ Passed Run_FunctionWithNullReturn_HandlesNullCorrectly [1 ms]
✅ Passed Run_FunctionThatThrowsException_CatchesAndReportsException [79 ms]
✅ Passed Run_FunctionWithComplexParameters_FormatsParametersCorrectly [4 ms]
✅ Passed Run_VoidMethodWithNoParameters_ExecutesCorrectly [1 ms]
✅ Passed Run_TimingMeasurement_IncludesExecutionTime [124 ms]

Test Run Successful.
Total tests: 10, Passed: 10, Failed: 0, Skipped: 0
Total time: 2.29 Seconds
```

### **Test Results Location**
- **Console Output**: Real-time results in terminal
- **TRX Files**: `test/TestResults/testresults.trx`
- **HTML Reports**: `test/TestResults/testresults.html`
- **VS Code Test Explorer**: Integrated results panel

## 🔍 Test Architecture

### **Helper Methods**

#### **Console Output Capture**
```csharp
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
```

This helper method captures `Console.WriteLine()` output from the Lab.Run() framework for verification in tests.

### **Test Method Pattern**

Each test follows the **Arrange-Act-Assert** pattern:

```csharp
[Test]
public void TestName_Scenario_ExpectedOutcome()
{
    // Arrange & Act
    var output = CaptureConsoleOutput(() =>
    {
        var result = Lab.Run(() => TestMethod(parameters));
        Assert.That(result, Is.EqualTo(expectedValue));
    });

    // Assert
    Assert.That(output, Does.Contain("expected output"));
}
```

### **Test Helper Methods**

The test class includes helper methods that simulate real-world scenarios:

- `TestVoidMethod()` - Void method with parameters
- `TestAddNumbers()` - Function returning int
- `TestStringConcat()` - Function returning string
- `TestCreateArray()` - Function returning array
- `TestCreateList()` - Function returning List<T>
- `TestReturnNull()` - Function returning null
- `TestThrowException()` - Method that throws exceptions
- `TestComplexParameters()` - Method with multiple parameter types
- `TestNoParameters()` - Parameterless method
- `TestWithDelay()` - Method with execution delay for timing tests

## 🎨 NUnit Assertion Syntax

### **Fluent Assertions**
```csharp
// Equality checks
Assert.That(result, Is.EqualTo(expected));
Assert.That(value, Is.Null);
Assert.That(value, Is.Not.Null);

// String assertions
Assert.That(output, Does.Contain("expected text"));
Assert.That(text, Does.StartWith("prefix"));
Assert.That(text, Does.EndWith("suffix"));

// Numeric assertions
Assert.That(number, Is.GreaterThan(10));
Assert.That(number, Is.LessThan(100));
Assert.That(number, Is.InRange(1, 50));

// Collection assertions
Assert.That(list, Has.Count.EqualTo(3));
Assert.That(array, Contains.Item("value"));
Assert.That(collection, Is.Empty);
```

## 🛠️ Configuration Files

### **runsettings.xml**
Configures test execution behavior:
- Test timeout settings
- Parallel execution options
- Output verbosity levels
- Code coverage collection
- Result file locations

### **packages.config**
Defines NuGet package versions for legacy project compatibility.

## 🔧 Debugging Tests

### **Setting Breakpoints**
1. Open test file in VS Code
2. Click in the gutter next to line numbers to set breakpoints
3. Use **"Debug Test"** CodeLens button or Command Palette
4. Step through code using F10 (step over) and F11 (step into)

### **Debugging Tips**
- **Examine Variables**: Hover over variables to see values
- **Watch Window**: Add expressions to monitor
- **Call Stack**: Navigate through method calls
- **Console Output**: View real-time console output during debugging

## 📈 Continuous Integration

The test configuration supports CI/CD pipelines:

```bash
# CI-friendly test command
dotnet test --logger trx --logger "console;verbosity=minimal" --collect:"XPlat Code Coverage"
```

This generates machine-readable results and code coverage reports suitable for automated build systems.

## 🎯 Best Practices

### **Writing New Tests**
1. **Follow naming convention**: `MethodName_Scenario_ExpectedOutcome`
2. **Use Arrange-Act-Assert pattern**
3. **Test one thing per test method**
4. **Use descriptive assertions with clear error messages**
5. **Include both positive and negative test cases**

### **Test Maintenance**
- **Keep tests independent** - no shared state between tests
- **Use helper methods** to reduce code duplication
- **Mock external dependencies** when needed
- **Regularly run full test suite** to catch regressions

## 🚀 Next Steps

To add new tests:

1. **Create test methods** with `[Test]` attribute
2. **Follow existing patterns** for consistency
3. **Test edge cases** and error conditions
4. **Verify console output** using `CaptureConsoleOutput()`
5. **Run tests** to ensure they pass

The testing framework is designed to grow with the project and maintain high quality code standards! 🎉
