namespace NUnitDemo.Core;

public class CollageService : ICollageService
{
    public string GetWelcomeNote(string name)
    {
        return $"Welcome to college, {name}!";
    }

    public string GetFareWellNote(string name)
    {
        return $"Goodbye, {name}! Best wishes!";
    }
}

