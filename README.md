# C# Brain Teasers Collection

A .NET console application for implementing and testing various C# brain teasers and coding challenges.

## Project Structure

```
CSharpBrainTeasers/
├── Program.cs                    # Main entry point
├── CSharpBrainTeasers.csproj    # Project file
├── tease.bat                    # Fun run command
└── BrainTeasers/                # Brain teaser categories
    ├── ArrayProblems.cs         # Array-related challenges
    ├── StringProblems.cs        # String manipulation challenges
    └── MathProblems.cs          # Mathematical algorithms
```

## How to Use

1. **Fun run**: `.\tease me` (PowerShell) or `tease me` (Command Prompt)
2. **Standard run**: `dotnet run`
3. **Add new brain teasers**: Create methods in the appropriate category class or add new category files
4. **Test specific problems**: Call the methods from `Program.cs` Main method

## Adding New Brain Teasers

### Method 1: Add to existing categories
Add static methods to `ArrayProblems.cs`, `StringProblems.cs`, or `MathProblems.cs`.

### Method 2: Create new categories
1. Create a new file in the `BrainTeasers` folder
2. Use the same namespace: `CSharpBrainTeasers.BrainTeasers`
3. Create a static class with static methods
4. Call your methods from `Program.cs`

### Method 3: Add to Program.cs
Add methods directly to the `Program` class for simple problems.

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

# Run the application
dotnet run

# Build the project
dotnet build
```

### Command Options
- **`.\tease me`** (PowerShell) or **`tease me`** (Command Prompt): Fun way to run brain teasers
- **`dotnet run`**: Standard .NET run command
- **`dotnet build`**: Compile the project without running

## Tips

- Use clear method names that describe the problem
- Add XML documentation comments (`///`) to explain each brain teaser
- Print both the problem description and solution
- Test with multiple examples where appropriate
