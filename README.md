# C# Brain Teasers Collection

A .NET console application for implementing and testing various C# brain teasers and coding challenges, featuring the powerful **X.Run()** execution analysis framework.

## ✨ Key Features

- **🔍 Automatic Method Analysis**: The `X.Run()` framework automatically extracts and displays method signatures, parameters, and execution details
- **⏱️ Performance Tracking**: Built-in execution timing and performance monitoring
- **🎨 Rich Console Output**: Beautiful ANSI-colored terminal output with emojis and formatting
- **🚨 Exception Handling**: Comprehensive error reporting with stack traces
- **🧪 Comprehensive Testing**: Full unit test suite demonstrating capabilities

## Project Structure

```
CSharpBrainTeasers/
├── src/                         # Source code
│   ├── Program.cs              # Main entry point
│   ├── X.cs                    # Execution analysis framework
│   └── BrainTeasers/           # Brain teaser categories
│       ├── ExampleTeasers.cs   # Example brain teasers
│       └── NothingToSeeHere.cs # Additional challenges
├── test/                       # Unit tests
│   ├── XTests.cs              # Comprehensive X.Run() tests
│   ├── GlobalUsings.cs        # Global using statements
│   └── CSharpBrainTeasers.Tests.csproj # Test project file
├── CSharpBrainTeasers.csproj   # Main project file
├── tease.bat                   # Fun run command
└── README.md                   # This file
```

## 🚀 The X.Run() Framework

The heart of this project is the `X.Run()` execution analysis framework that provides automatic method introspection and beautiful output formatting.

### Basic Usage

```csharp
// Automatically analyzes and executes any method
X.Run(() => YourMethod(param1, param2));

// Works with void methods
X.Run(() => SomeVoidMethod(42, "test"));

// Works with methods that return values
var result = X.Run(() => CalculateSomething(10, 20));
```

### Example Output

```
=== Execution Analysis ===
Program.ExampleDivideNumbers(Int32 a = 10, Int32 b = 2)

⏳ Executing...
Brain Teaser #1: Divide Numbers
Solution: 5

✅ Execution completed successfully!
⏱️  Execution time: 45.61 ms
📋 Result: void

=== End Analysis ===
```

### Features of X.Run()

- **🔍 Automatic Parameter Detection**: Extracts parameter names and values
- **🎯 Type Information**: Shows parameter and return types
- **⏱️ Execution Timing**: Measures and reports execution time
- **🚨 Exception Handling**: Catches and beautifully formats exceptions
- **📊 Result Formatting**: Smart formatting for arrays, collections, and objects
- **🎨 ANSI Colors**: Rich terminal colors and emoji indicators

## How to Use

### Running the Main Application

1. **Fun run**: `.\tease me` (PowerShell) or `tease me` (Command Prompt)
2. **Standard run**: `dotnet run`

### Running Tests

```bash
# Run all tests
cd test
dotnet test

# Run tests with verbose output
dotnet test --logger "console;verbosity=normal"

# Run tests with detailed output and trx file
dotnet test --logger trx --logger "console;verbosity=detailed"

# Run a specific test
dotnet test --filter "Run_VoidMethod_ExecutesSuccessfully"

# Run all X.Run() tests
dotnet test --filter "XTests"

# Watch mode - automatically run tests when files change
dotnet test --watch
```

### VS Code Testing Integration

The project is configured for optimal VS Code testing experience with **NUnit 4.0.1**:

1. **Test Explorer**: Tests automatically appear in the Test Explorer panel
2. **CodeLens**: Run/Debug buttons appear above each test method
3. **Debugging**: Set breakpoints and debug tests directly
4. **Live Testing**: Use watch mode for continuous testing
5. **Test Results**: Comprehensive reporting with trx and html output

**VS Code Commands:**
- `Ctrl+Shift+P` → "Test: Run All Tests"
- `Ctrl+Shift+P` → "Test: Debug All Tests" 
- `Ctrl+Shift+P` → "Tasks: Run Task" → "test" (run tests)
- `Ctrl+Shift+P` → "Tasks: Run Task" → "test-watch" (continuous testing)

## Adding New Brain Teasers

### Method 1: Using X.Run() (Recommended)
```csharp
static void Main(string[] args)
{
    Console.WriteLine("=== C# Brain Teasers Collection ===");
    Console.WriteLine();

    // Simply wrap your method call with X.Run()
    X.Run(() => YourBrainTeaser(input1, input2));
    X.Run(() => AnotherTeaser("test", 42));
}

static void YourBrainTeaser(int a, int b)
{
    Console.WriteLine($"Brain Teaser: Add two numbers");
    Console.WriteLine($"Solution: {a + b}");
}
```

### Method 2: Add to existing categories
Add static methods to existing brain teaser categories in the `src/BrainTeasers/` folder.

### Method 3: Create new categories
1. Create a new file in the `src/BrainTeasers/` folder
2. Use the namespace: `CSharpBrainTeasers.BrainTeasers`
3. Create a static class with static methods
4. Call your methods from `Program.cs` using `X.Run()`

## Testing Your Code

The project includes comprehensive unit tests that demonstrate:

- ✅ Void method execution
- ✅ Methods with return values
- ✅ String formatting and escaping
- ✅ Array and collection handling
- ✅ Null value handling
- ✅ Exception catching and reporting
- ✅ Complex parameter types
- ✅ Execution timing validation

### Running Specific Tests

```bash
# Run a specific test
dotnet test --filter "Run_VoidMethod_ExecutesSuccessfully"

# Run all X.Run() tests
dotnet test --filter "XTests"
```

## Running the Project

### Fun Command (Recommended)
```powershell
# PowerShell
.\tease me

# Command Prompt (cmd)
tease me
```

### Standard .NET Commands
```bash
# Navigate to project directory

# Run the main application
dotnet run

# Build the project
dotnet build

# Run tests
cd test
dotnet test
```

### Command Options
- **`.\tease me`** (PowerShell) or **`tease me`** (Command Prompt): Fun way to run brain teasers
- **`dotnet run`**: Standard .NET run command
- **`dotnet build`**: Compile the project without running
- **`dotnet test`**: Run the comprehensive unit test suite

## 💡 Tips

- **Use X.Run()** for all method executions to get beautiful analysis output
- Use clear method names that describe the problem
- Add XML documentation comments (`///`) to explain each brain teaser
- Print both the problem description and solution within your methods
- Test with multiple examples where appropriate
- The X.Run() framework automatically handles timing, formatting, and error reporting

## 🎯 Example Brain Teaser

```csharp
static void Main(string[] args)
{
    Console.WriteLine("=== C# Brain Teasers Collection ===");
    Console.WriteLine();

    // The X.Run framework automatically analyzes this call
    X.Run(() => FibonacciSequence(10));
}

static void FibonacciSequence(int n)
{
    Console.WriteLine($"Brain Teaser: Generate Fibonacci sequence up to {n}");
    
    int a = 0, b = 1;
    Console.Write($"Fibonacci sequence: {a}, {b}");
    
    for (int i = 2; i < n; i++)
    {
        int next = a + b;
        Console.Write($", {next}");
        a = b;
        b = next;
    }
    Console.WriteLine();
}
```

This will automatically produce beautiful output showing the method signature, execution time, and results!

## 🔧 Development

- **Framework**: .NET 9.0
- **Testing**: xUnit with comprehensive unit tests
- **Architecture**: Clean separation between source code (`src/`) and tests (`test/`)
- **Special Features**: Expression tree analysis for automatic method introspection
