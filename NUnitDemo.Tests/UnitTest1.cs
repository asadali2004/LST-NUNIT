using NUnit.Framework;
using NUnitDemo.Core;

namespace NUnitDemo.Tests;

[TestFixture]
public class CalculatorTests
{
    private Calculator _calc;

    [SetUp] // This runs before every test method
    public void Setup()
    {
        _calc = new Calculator();
    }

    [Test]
    public void All_Assertion_Examples()
    {
        // 1. Equality Assertions
        Assert.That(_calc.Add(2, 2), Is.EqualTo(4));
        Assert.That(_calc.Add(2, 2), Is.Not.EqualTo(5));

        // 2. Numeric Comparisons
        Assert.That(10, Is.GreaterThan(5));
        Assert.That(10, Is.LessThan(20));
        Assert.That(10, Is.InRange(1, 100));

        // 3. Boolean & Nulls
        Assert.That(true, Is.True);
        Assert.That(false, Is.False);
        Assert.That((string?)null, Is.Null);
        Assert.That("test", Is.Not.Null);

        // 4. String Assertions
        string msg = "Hello World";
        Assert.That(msg, Does.Contain("World"));
        Assert.That(msg, Does.StartWith("Hello"));
        Assert.That(msg, Does.EndWith("World"));

        // 5. Collection Assertions
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list, Has.Count.EqualTo(3));
        Assert.That(list, Has.Member(2));
        Assert.That(list, Is.Not.Empty);

        // 6. Exception Assertions
        Assert.Throws<ArgumentException>(() => _calc.Divide(10, 0));
    }

    // 7. Parameterized Tests (Running the same test with different data)
    [TestCase(1, 2, 3)]
    [TestCase(-1, 1, 0)]
    [TestCase(100, 200, 300)]
    public void Add_MultipleInputs_ReturnsExpected(int a, int b, int expected)
    {
        Assert.That(_calc.Add(a, b), Is.EqualTo(expected));
    }
}
