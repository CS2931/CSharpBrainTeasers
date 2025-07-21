@echo off
if "%1"=="me" (
    echo Running C# Brain Teasers...
    dotnet run
) else (
    echo Usage: tease me
    echo This will run the C# Brain Teasers project
)
