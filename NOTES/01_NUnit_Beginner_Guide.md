# NUnit Testing - Beginner's Guide

## What is NUnit?

**NUnit** is a testing framework for .NET that helps you verify your code works correctly before shipping to users.

**Why use it?**
- ✅ Catch bugs early (cheaper to fix)
- ✅ Refactor safely with test coverage
- ✅ Document how code should behave
- ✅ Industry standard and widely used

---

## Setup (5 Minutes)

### Step 1: Create Solution
```bash
dotnet new sln -n MyApp
cd MyApp
```

### Step 2: Create Production Project
```bash
dotnet new classlib -n MyApp.Core
```

### Step 3: Create Test Project
```bash
dotnet new nunit -n MyApp.Tests
```

### Step 4: Link Projects
```bash
dotnet sln add MyApp.Core MyApp.Tests
cd MyApp.Tests
dotnet add reference ../MyApp.Core/MyApp.Core.csproj
cd ..
```

### Step 5: Run Tests
```bash
dotnet test
```

---

## Your First Test

### Production Code (MyApp.Core/Calculator.cs)
```csharp
namespace MyApp.Core
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new ArgumentException("Cannot divide by zero");
            return a / b;
        }
    }
}
```

### Test Code (MyApp.Tests/CalculatorTests.cs)
```csharp
using NUnit.Framework;
using MyApp.Core;

namespace MyApp.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calc;

        // Runs before EACH test
        [SetUp]
        public void Setup()
        {
            _calc = new Calculator();
        }

        // This is a test method
        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // ARRANGE - setup
            int a = 5;
            int b = 3;

            // ACT - execute
            int result = _calc.Add(a, b);

            // ASSERT - verify
            Assert.That(result, Is.EqualTo(8));
        }

        // Multiple test cases - runs 3 times
        [TestCase(2, 3, 5)]
        [TestCase(10, 20, 30)]
        [TestCase(-5, 5, 0)]
        public void Add_WithVariousNumbers_ReturnsCorrectSum(int a, int b, int expected)
        {
            int result = _calc.Add(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        // Test that exception is thrown
        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => _calc.Divide(10, 0)
            );
            Assert.That(ex.Message, Does.Contain("Cannot divide by zero"));
        }

        // Cleanup after test
        [TearDown]
        public void Cleanup()
        {
            _calc = null;
        }
    }
}
```

Run tests:
```bash
dotnet test
```

---

## Key Test Attributes

| Attribute | When | Example |
|-----------|------|---------|
| `[TestFixture]` | Mark class as test class | `[TestFixture] public class MyTests` |
| `[Test]` | Mark method as test | `[Test] public void MyTest()` |
| `[SetUp]` | Before EACH test | Initialize objects |
| `[TearDown]` | After EACH test | Cleanup resources |
| `[TestCase(...)]` | Run test with different data | `[TestCase(1,2,3)]` |
| `[OneTimeSetUp]` | Before ALL tests (once) | Expensive setup |
| `[OneTimeTearDown]` | After ALL tests (once) | Final cleanup |
| `[Category("name")]` | Group tests | `[Category("Math")]` |
| `[Ignore("reason")]` | Skip this test | `[Ignore("Not ready")]` |

---

## Common Assertions

```csharp
// Equality
Assert.That(value, Is.EqualTo(5));
Assert.That(value, Is.Not.EqualTo(0));

// Comparisons
Assert.That(value, Is.GreaterThan(0));
Assert.That(value, Is.LessThan(100));
Assert.That(value, Is.InRange(1, 10));

// Boolean
Assert.That(condition, Is.True);
Assert.That(condition, Is.False);

// Null
Assert.That(obj, Is.Null);
Assert.That(obj, Is.Not.Null);

// Strings
Assert.That(text, Does.StartWith("Hello"));
Assert.That(text, Does.EndWith("!"));
Assert.That(text, Does.Contain("world"));

// Collections
Assert.That(list, Has.Count.EqualTo(5));
Assert.That(list, Has.Member(item));
Assert.That(list, Is.Empty);

// Exceptions
Assert.Throws<Exception>(() => method());
Assert.DoesNotThrow(() => method());
```

---

## Arrange-Act-Assert Pattern

Every test should follow this structure:

```csharp
[Test]
public void MethodName_Scenario_ExpectedResult()
{
    // ARRANGE - Set up test data
    var obj = new MyClass();
    int input = 5;

    // ACT - Execute the method
    int result = obj.Calculate(input);

    // ASSERT - Verify result
    Assert.That(result, Is.EqualTo(10));
}
```

---

## Test Naming Convention

Good names explain what's being tested:

```
MethodName_Scenario_ExpectedResult
```

**Examples:**
```csharp
Add_TwoPositiveNumbers_ReturnsSum()
Divide_ByZero_ThrowsException()
ValidateEmail_WithInvalidEmail_ReturnsFalse()
GetUser_WithInvalidId_ReturnsNull()
```

**Bad names:** ❌
```csharp
Test1()
DoTest()
TestSomething()
```

---

## Best Practices

### ✅ DO's

1. **One test = One behavior**
   ```csharp
   // Good - tests one thing
   [Test]
   public void Add_ReturnsSum() { }

   // Bad - tests multiple things
   [Test]
   public void Calculate_Test()
   {
       var result1 = Add(2, 3);
       var result2 = Multiply(2, 3);
       var result3 = Divide(2, 3);
   }
   ```

2. **Keep tests independent** - no test should depend on another
   ```csharp
   [Test]
   public void Test1() { /* complete setup */ }
   
   [Test]
   public void Test2() { /* complete setup */ }
   ```

3. **Use SetUp for common initialization**
   ```csharp
   private Calculator _calc;
   
   [SetUp]
   public void Setup()
   {
       _calc = new Calculator();
   }
   ```

4. **Use descriptive variable names**
   ```csharp
   // Good
   decimal salary = 5000m;
   decimal expectedBonus = 750m;
   
   // Bad
   decimal a = 5000;
   decimal b = 750;
   ```

5. **Test edge cases**
   ```csharp
   [TestCase(0)]        // Edge
   [TestCase(1)]        // Small
   [TestCase(999)]      // Large
   [TestCase(-1)]       // Negative
   public void IsPositive_WithVariousNumbers(int num)
   {
       // test all scenarios
   }
   ```

### ❌ DON'Ts

1. ❌ Don't test multiple things in one test
2. ❌ Don't use random test data
3. ❌ Don't test private methods (test public behavior)
4. ❌ Don't print to console (use assertions)
5. ❌ Don't skip AAA pattern
6. ❌ Don't create tests that depend on other tests

---

## Running Tests

### Command Line
```bash
# All tests
dotnet test

# Specific test file
dotnet test MyApp.Tests.csproj

# By category
dotnet test --filter "Category=Math"

# Verbose output
dotnet test --verbosity detailed
```

### Visual Studio
1. Open **Test Explorer** (Test → Test Explorer)
2. Click **Run All Tests**
3. Right-click test → **Debug** to step through

---

## Exception Testing

### Verify Exception is Thrown
```csharp
[Test]
public void Divide_ByZero_ThrowsException()
{
    var calc = new Calculator();
    Assert.Throws<DivideByZeroException>(
        () => calc.Divide(10, 0)
    );
}
```

### Verify Exception Message
```csharp
[Test]
public void Divide_ByZero_HasCorrectMessage()
{
    var calc = new Calculator();
    
    var ex = Assert.Throws<DivideByZeroException>(
        () => calc.Divide(10, 0)
    );
    
    Assert.That(ex.Message, Does.Contain("zero"));
}
```

### Verify No Exception is Thrown
```csharp
[Test]
public void ValidInput_DoesNotThrow()
{
    var calc = new Calculator();
    Assert.DoesNotThrow(() => calc.Divide(10, 2));
}
```

---

## Debugging Tests

1. **Set Breakpoint** - Click margin next to line number
2. **Run in Debug** - Right-click test → Debug
3. **Inspect Variables** - Hover over variables
4. **Step Through** - F10 (Step Over) or F11 (Step Into)

---

## Quick Reference

### Test Attributes
```csharp
[TestFixture]          // Test class
[Test]                 // Test method
[SetUp]                // Before each test
[TearDown]             // After each test
[OneTimeSetUp]         // Before all tests
[OneTimeTearDown]      // After all tests
[TestCase(1, 2, 3)]   // Parameterized test
[Ignore("reason")]     // Skip test
[Category("name")]     // Tag/group test
```

### Common Assertions
```csharp
Is.EqualTo(x)          // Equal to
Is.Not.EqualTo(x)      // Not equal
Is.GreaterThan(x)      // Greater than
Is.LessThan(x)         // Less than
Is.Null / Is.Not.Null  // Null check
Is.True / Is.False     // Boolean
Does.Contain(x)        // String/Collection contains
Has.Count.EqualTo(x)   // Collection count
```

---

## Complete Example

```csharp
[TestFixture]
public class BankAccountTests
{
    private BankAccount _account;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // Setup once for all tests
    }

    [SetUp]
    public void Setup()
    {
        _account = new BankAccount(1000m);  // Start with $1000
    }

    [Test]
    public void Deposit_AddsMoneyToAccount()
    {
        _account.Deposit(500m);
        Assert.That(_account.Balance, Is.EqualTo(1500m));
    }

    [TestCase(200m)]
    [TestCase(500m)]
    [TestCase(1000m)]
    public void Withdraw_ValidAmount_DeductsMoney(decimal amount)
    {
        _account.Withdraw(amount);
        Assert.That(_account.Balance, Is.EqualTo(1000m - amount));
    }

    [Test]
    public void Withdraw_MoreThanBalance_ThrowsException()
    {
        var ex = Assert.Throws<InvalidOperationException>(
            () => _account.Withdraw(2000m)
        );
        Assert.That(ex.Message, Does.Contain("Insufficient"));
    }

    [Test]
    public void GetBalance_ReturnsCurrentBalance()
    {
        Assert.That(_account.Balance, Is.EqualTo(1000m));
    }

    [TearDown]
    public void Cleanup()
    {
        _account = null;
    }

    [OneTimeTearDown]
    public void OneTimeCleanup()
    {
        // Final cleanup
    }
}
```

---

## Next Steps

1. **Write tests for your code** - Start with simple methods
2. **Use [TestCase] for variations** - Test multiple scenarios
3. **Follow AAA pattern** - Organize every test clearly
4. **Run tests frequently** - Use `dotnet test --watch`
5. **Learn Mocking** - Test with dependencies using Moq
6. **Measure Coverage** - Use `dotnet test /p:CollectCoverage=true`

---

## Resources

- **NUnit Docs**: https://nunit.org
- **Assertions Reference**: https://nunit.org/docs/3.0/Assertions.html
- **Microsoft Testing Guide**: https://learn.microsoft.com/en-us/dotnet/core/testing
