namespace CSharpBrainTeasers.BrainTeasers;

public static class NothingToSeeHere
{
    private static string[] ParseParts(string? input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException("Input cannot be empty", nameof(input)),
            _ => input.Split(';')
        };
    }
}