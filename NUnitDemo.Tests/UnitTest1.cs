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
    int sum = _calc.Add(2, 2);
    Assert.That(sum, Is.EqualTo(4));
    Assert.That(sum, Is.Not.EqualTo(5));

    // 2. Numeric Comparisons
    int number = _calc.Add(7, 3);
    Assert.That(number, Is.GreaterThan(5));
    Assert.That(number, Is.LessThan(20));
    Assert.That(number, Is.InRange(1, 100));

    // 3. Boolean & Nulls
    string greeting = _calc.GetGreeting("Ali");
    Assert.That(greeting, Is.Not.Null);
    Assert.That(greeting.Contains("Ali"), Is.True);

    // 4. String Assertions
    Assert.That(greeting, Does.StartWith("Hello"));
    Assert.That(greeting, Does.EndWith("!"));

    // 5. Collection Assertions
    var list = new List<int> { sum, number };
    Assert.That(list, Has.Count.EqualTo(2));
    Assert.That(list, Has.Member(4));
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
