# NUnit – Complete and Detailed Guide

> 📌 **Start Here:** See [NUnit_Beginner_Guide.md](NUnit_Beginner_Guide.md) for a quick introduction!
> This guide covers comprehensive topics and real-world examples.

---

## 1. What is NUnit?

**NUnit** is a unit testing framework for .NET applications. It helps developers verify code correctness, detect bugs early, and maintain code quality.

**Key Features:**
- Attribute-based testing (`[Test]`, `[SetUp]`, etc.)
- Constraint-based assertions (`Assert.That`)
- Parameterized tests (`[TestCase]`)
- Test organization and filtering
- Integration with Visual Studio and CI/CD pipelines

---

## 2. Project Architecture

**Rule:** Keep tests separate from production code

```
Solution/
├── MyApp.Core/              ← Production code
│   ├── Calculator.cs
│   └── MyApp.Core.csproj
│
└── MyApp.Tests/             ← Test code
    ├── CalculatorTests.cs
    └── MyApp.Tests.csproj
```

**Why separate?**
- Prevents test code shipping to production
- Keeps production DLLs smaller
- Allows different dependencies for tests
- Clear separation of concerns

---

## 3. Setup

### Create Project Structure
```bash
dotnet new sln -n MyApp
cd MyApp
dotnet new classlib -n MyApp.Core
dotnet new nunit -n MyApp.Tests
dotnet sln add MyApp.Core MyApp.Tests
cd MyApp.Tests
dotnet add reference ../MyApp.Core/MyApp.Core.csproj
```

### Automatic NUnit Dependencies
- **NUnit** - Core testing framework
- **NUnit3TestAdapter** - Visual Studio integration
- **Microsoft.NET.Test.Sdk** - Test discovery and execution

---

## 4. Test Class Structure

```csharp
using NUnit.Framework;
using MyApp.Core;

namespace MyApp.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calc;

        // Before each test
        [SetUp]
        public void Setup()
        {
            _calc = new Calculator();
        }

        // The actual test
        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // ARRANGE
            int a = 5, b = 3;

            // ACT
            int result = _calc.Add(a, b);

            // ASSERT
            Assert.That(result, Is.EqualTo(8));
        }

        // After each test
        [TearDown]
        public void Cleanup()
        {
            _calc = null;
        }
    }
}
```

---

## 5. Test Attributes

| Attribute | Purpose | When Called |
|-----------|---------|-------------|
| `[TestFixture]` | Marks test class | (metadata) |
| `[Test]` | Marks test method | Per method |
| `[SetUp]` | Initialize before test | Before each `[Test]` |
| `[TearDown]` | Cleanup after test | After each `[Test]` |
| `[OneTimeSetUp]` | One-time setup | Before all tests |
| `[OneTimeTearDown]` | One-time cleanup | After all tests |
| `[TestCase(x,y,z)]` | Parameterized test | Run with each set of values |
| `[Category("name")]` | Group tests | For filtering |
| `[Ignore("reason")]` | Skip test | Not executed |
| `[Timeout(ms)]` | Time limit | Fail if exceeds |

### Execution Order

```
[OneTimeSetUp]     ← Runs once at start
[SetUp]            ← Before test 1
[Test1]
[TearDown]         ← After test 1
[SetUp]            ← Before test 2
[Test2]
[TearDown]         ← After test 2
[OneTimeTearDown]  ← Runs once at end
```

---

## 6. Assertions (NUnit 4 Style)

### Equality
```csharp
Assert.That(value, Is.EqualTo(5));
Assert.That(value, Is.Not.EqualTo(0));
```

### Comparisons
```csharp
Assert.That(value, Is.GreaterThan(0));
Assert.That(value, Is.LessThan(100));
Assert.That(value, Is.GreaterThanOrEqualTo(5));
Assert.That(value, Is.InRange(1, 10));
```

### Boolean
```csharp
Assert.That(condition, Is.True);
Assert.That(condition, Is.False);
```

### Null Checks
```csharp
Assert.That(obj, Is.Null);
Assert.That(obj, Is.Not.Null);
```

### Strings
```csharp
Assert.That(text, Does.StartWith("Hello"));
Assert.That(text, Does.EndWith("World"));
Assert.That(text, Does.Contain("test"));
Assert.That(text, Is.Empty);
```

### Collections
```csharp
Assert.That(list, Has.Count.EqualTo(5));
Assert.That(list, Has.Member(item));
Assert.That(list, Is.Empty);
Assert.That(list, Is.Ordered.Ascending);
```

### Exceptions
```csharp
Assert.Throws<ArgumentException>(() => method());
var ex = Assert.Throws<InvalidOperationException>(() => method());
Assert.That(ex.Message, Does.Contain("error"));
Assert.DoesNotThrow(() => method());
```

---

## 7. Parameterized Tests

Run the same test with different data:

```csharp
[TestCase(2, 3, 5)]
[TestCase(10, 20, 30)]
[TestCase(-5, 5, 0)]
public void Add_WithVariousNumbers_ReturnsCorrectSum(int a, int b, int expected)
{
    var result = _calc.Add(a, b);
    Assert.That(result, Is.EqualTo(expected));
}
```

Output:
```
✓ Add_WithVariousNumbers_ReturnsCorrectSum(2,3,5)
✓ Add_WithVariousNumbers_ReturnsCorrectSum(10,20,30)
✓ Add_WithVariousNumbers_ReturnsCorrectSum(-5,5,0)
```

### With Descriptions
```csharp
[TestCase(2, 3, 5, Description = "Small numbers")]
[TestCase(100, 200, 300, Description = "Large numbers")]
public void Add_Test(int a, int b, int expected)
{
    Assert.That(_calc.Add(a, b), Is.EqualTo(expected));
}
```

---

## 8. Best Practices

### ✅ DO's

1. **Follow AAA Pattern**
   ```csharp
   [Test]
   public void Test()
   {
       // ARRANGE
       var obj = new MyClass();
       
       // ACT
       var result = obj.Method();
       
       // ASSERT
       Assert.That(result, Is.EqualTo(expected));
   }
   ```

2. **One assertion focus per test**
   ```csharp
   [Test]
   public void Add_ReturnsSum() { /* test one thing */ }
   
   [Test]
   public void Add_IsCommutative() { /* test another thing */ }
   ```

3. **Use descriptive names**
   ```csharp
   Add_TwoNumbers_ReturnsSum()        // ✅ Clear
   Divide_ByZero_ThrowsException()    // ✅ Clear
   Test1()                             // ❌ Unclear
   ```

4. **Keep tests independent**
   ```csharp
   [Test]
   public void Test1() { /* complete setup */ }
   
   [Test]
   public void Test2() { /* complete setup */ }
   // Test2 doesn't depend on Test1
   ```

5. **Use SetUp for common initialization**
   ```csharp
   [SetUp]
   public void Init()
   {
       _obj = new MyClass();
   }
   ```

6. **Test edge cases**
   ```csharp
   [TestCase(0)]
   [TestCase(1)]
   [TestCase(-1)]
   [TestCase(int.MaxValue)]
   public void IsPositive_Test(int value) { }
   ```

### ❌ DON'Ts

1. ❌ Test multiple behaviors in one test
2. ❌ Use random test data
3. ❌ Test private methods
4. ❌ Print to console (use assertions)
5. ❌ Skip AAA pattern
6. ❌ Create test interdependencies
7. ❌ Use magic numbers without context

---

## 9. Running Tests

### Command Line
```bash
# All tests
dotnet test

# Specific project
dotnet test MyApp.Tests.csproj

# By category
dotnet test --filter "Category=Math"

# Verbose
dotnet test --verbosity detailed

# Watch mode (auto-run on changes)
dotnet test --watch
```

### Visual Studio
1. **Test Explorer** → View → Test Explorer (Ctrl+E, T)
2. **Run All** → Click ▶️
3. **Debug** → Right-click test → Debug
4. **Filter** → Use search box

---

## 10. Exception Testing

### Basic Exception Verification
```csharp
[Test]
public void Divide_ByZero_ThrowsException()
{
    Assert.Throws<DivideByZeroException>(
        () => _calc.Divide(10, 0)
    );
}
```

### Verify Exception Details
```csharp
[Test]
public void Divide_ByZero_HasCorrectMessage()
{
    var ex = Assert.Throws<ArgumentException>(
        () => _calc.Divide(10, 0)
    );
    
    Assert.That(ex.Message, Does.Contain("zero"));
    Assert.That(ex.ParamName, Is.EqualTo("divisor"));
}
```

### Verify No Exception
```csharp
[Test]
public void ValidInput_DoesNotThrow()
{
    Assert.DoesNotThrow(() => _calc.Divide(10, 2));
}
```

---

## 11. Debugging Tests

### Set Breakpoint
Click margin next to line number to set a breakpoint (red dot appears)

### Debug Specific Test
Right-click test → **Debug**

### Step Through Code
- **F10** - Step Over (next line)
- **F11** - Step Into (inside function)
- **Shift+F11** - Step Out (exit function)

### Inspect Variables
Hover over variables to see their values

---

## 12. Test Naming Convention

Use pattern: `MethodName_Scenario_ExpectedResult`

```csharp
// Good examples
Add_TwoPositiveNumbers_ReturnsSum()
Divide_ByZero_ThrowsException()
ValidateEmail_WithInvalidEmail_ReturnsFalse()
GetUser_WithNonExistentId_ReturnsNull()

// Bad examples
Test1()
TestAdd()
DoTest()
TestFunction()
```

---

## 13. Common Testing Mistakes

### ❌ Multiple Assertions for Different Things
```csharp
// Bad
[Test]
public void UserService_Test()
{
    var user = service.CreateUser("john@email.com");
    Assert.That(user, Is.Not.Null);
    Assert.That(user.Email, Is.EqualTo("john@email.com"));
    Assert.That(user.CreatedDate, Is.Not.EqualTo(default(DateTime)));
    // Three things tested
}

// Good - separate tests
[Test]
public void CreateUser_ReturnsUser() { }

[Test]
public void CreateUser_SetsEmail() { }
```

### ❌ Only Happy Path Testing
```csharp
// Bad - no edge cases
[TestCase(10, 5)]
public void Divide_Test(int a, int b)
{
    Assert.That(Divide(a, b), Is.GreaterThan(0));
}

// Good - comprehensive
[TestCase(10, 5, 2)]
[TestCase(20, 4, 5)]
[TestCase(0, 5, 0)]
public void Divide_Test(int a, int b, int expected)
{
    Assert.That(Divide(a, b), Is.EqualTo(expected));
}

[Test]
public void Divide_ByZero_Throws()
{
    Assert.Throws<DivideByZeroException>(() => Divide(10, 0));
}
```

### ❌ Magic Numbers
```csharp
// Bad - unclear meaning
[Test]
public void Calculate_Test()
{
    Assert.That(Calculate(42, 8, 350), Is.EqualTo(400));
}

// Good - clear context
[Test]
public void CalculateOrderTotal_WithTax_ReturnsTotal()
{
    decimal subtotal = 42m;
    decimal discount = 8m;
    decimal tax = 350m;
    decimal expected = 400m;
    
    Assert.That(Calculate(subtotal, discount, tax), Is.EqualTo(expected));
}
```

---

## 14. Test Organization

### By Class
```
Tests/
├── CalculatorTests.cs       // Tests for Calculator
├── ValidatorTests.cs        // Tests for Validator
└── PaymentTests.cs          // Tests for Payment
```

### By Folder
```
Tests/
├── Unit/
│   ├── CalculatorTests.cs
│   └── ValidatorTests.cs
│
└── Integration/
    ├── DatabaseTests.cs
    └── ApiTests.cs
```

### By Category
```csharp
[Test, Category("Math")]
public void Add_Test() { }

[Test, Category("Math")]
public void Multiply_Test() { }

[Test, Category("Validation")]
public void ValidateEmail_Test() { }
```

Run specific category:
```bash
dotnet test --filter "Category=Math"
```

---

## 15. Real-World Example

```csharp
[TestFixture]
public class BankAccountTests
{
    private BankAccount _account;

    [SetUp]
    public void Setup()
    {
        _account = new BankAccount(startingBalance: 1000m);
    }

    [Test]
    public void Deposit_WithValidAmount_IncreasesBalance()
    {
        _account.Deposit(500m);
        Assert.That(_account.Balance, Is.EqualTo(1500m));
    }

    [TestCase(100m)]
    [TestCase(500m)]
    [TestCase(1000m)]
    public void Withdraw_WithValidAmount_DecreasesBalance(decimal amount)
    {
        _account.Withdraw(amount);
        Assert.That(_account.Balance, Is.EqualTo(1000m - amount));
    }

    [Test]
    public void Withdraw_ExceedingBalance_ThrowsException()
    {
        var ex = Assert.Throws<InvalidOperationException>(
            () => _account.Withdraw(2000m)
        );
        
        Assert.That(ex.Message, Does.Contain("Insufficient funds"));
        Assert.That(_account.Balance, Is.EqualTo(1000m), "Balance should not change");
    }

    [TestCase(0)]
    [TestCase(-100)]
    public void Deposit_WithInvalidAmount_ThrowsException(decimal amount)
    {
        Assert.Throws<ArgumentException>(() => _account.Deposit(amount));
    }

    [Test, Category("Edge Cases")]
    public void MultipleTransactions_UpdatesBalanceCorrectly()
    {
        _account.Deposit(500m);
        Assert.That(_account.Balance, Is.EqualTo(1500m));
        
        _account.Withdraw(300m);
        Assert.That(_account.Balance, Is.EqualTo(1200m));
        
        _account.Withdraw(200m);
        Assert.That(_account.Balance, Is.EqualTo(1000m));
    }
}
```

---

## Quick Reference Card

### Attributes
```
[TestFixture]          [Test]              [SetUp]
[TearDown]             [OneTimeSetUp]      [OneTimeTearDown]
[TestCase(...)]        [Category("...")]   [Ignore("...")]
```

### Common Assertions
```
Is.EqualTo(x)          Is.Not.EqualTo(x)   Is.GreaterThan(x)
Is.LessThan(x)         Is.InRange(a, b)    Is.Null / Is.Not.Null
Is.True / Is.False     Does.Contain(x)     Does.StartWith(x)
Has.Count.EqualTo(x)   Has.Member(x)       Is.Empty
```

---

## Next Steps

1. **Write tests for your code** - Start with simple methods
2. **Use parameterized tests** - Test multiple scenarios efficiently
3. **Master AAA pattern** - Keep tests organized
4. **Learn exception testing** - Verify error conditions
5. **Explore mocking** - Test with Moq framework
6. **Measure code coverage** - Use Coverlet
7. **Setup CI/CD** - Run tests automatically

---

## Resources

- **Official NUnit**: https://nunit.org
- **Assertions**: https://nunit.org/docs/3.0/Assertions.html
- **GitHub**: https://github.com/nunit/nunit
- **Microsoft Guide**: https://learn.microsoft.com/en-us/dotnet/core/testing
