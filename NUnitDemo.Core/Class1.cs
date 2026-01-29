namespace NUnitDemo.Core;

public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new ArgumentException("Cannot divide by zero", nameof(b));
        return a / b;
    }
}
