# 🧪 NUnit Testing Demo Project

A complete NUnit testing project demonstrating unit testing best practices in .NET.

---

## 📁 Project Structure

```
NUnitDemo/
├── NUnitDemo.slnx              ← Solution file
│
├── NUnitDemo.Core/             ← Production Code
│   ├── Class1.cs              ← Calculator class (Add, Divide methods)
│   ├── NUnitDemo.Core.csproj
│   ├── bin/
│   └── obj/
│
├── NUnitDemo.Tests/            ← Test Code (NUnit Tests)
│   ├── UnitTest1.cs           ← CalculatorTests class
│   ├── NUnitDemo.Tests.csproj
│   ├── bin/
│   └── obj/
│
├── README.md                   ← This file
└── NOTES/                      ← Documentation (guides)
```

---

## 🚀 Quick Start

### 1. Open the Project
```bash
cd e:\DotNet_Learning\C#_Practice_Projects\NUnitDemo
```

### 2. Run Tests
```bash
dotnet test
```

### 3. View Test Results
Look for output showing:
- ✅ Tests passed
- Assertions verified
- Parameterized tests executed

---

## 📝 Project Contents

### **NUnitDemo.Core/** (Production Code)

**File:** `Class1.cs`
- **Class:** `Calculator`
- **Methods:**
  - `Add(int a, int b)` → Returns sum of two numbers
  - `Divide(int a, int b)` → Returns division result
    - Throws `ArgumentException` if b == 0

```csharp
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
```

---

### **NUnitDemo.Tests/** (Test Code)

**File:** `UnitTest1.cs`
- **Test Class:** `CalculatorTests`
- **Test Methods:**

#### 1. `All_Assertion_Examples()` - Demonstrates all assertion types
```csharp
// 1. Equality Assertions
Assert.That(result, Is.EqualTo(expected));
Assert.That(result, Is.Not.EqualTo(wrong));

// 2. Numeric Comparisons
Assert.That(value, Is.GreaterThan(5));
Assert.That(value, Is.LessThan(100));
Assert.That(value, Is.InRange(1, 100));

// 3. Boolean & Nulls
Assert.That(true, Is.True);
Assert.That(null, Is.Null);

// 4. String Assertions
Assert.That(msg, Does.Contain("text"));
Assert.That(msg, Does.StartWith("Hello"));

// 5. Collection Assertions
Assert.That(list, Has.Count.EqualTo(3));
Assert.That(list, Has.Member(item));

// 6. Exception Assertions
Assert.Throws<ArgumentException>(() => method());
```

#### 2. `Add_MultipleInputs_ReturnsExpected()` - Parameterized test
```csharp
[TestCase(1, 2, 3)]      // Runs with these values
[TestCase(-1, 1, 0)]     // Runs with these values
[TestCase(100, 200, 300)]  // Runs with these values
public void Add_MultipleInputs_ReturnsExpected(int a, int b, int expected)
{
    Assert.That(_calc.Add(a, b), Is.EqualTo(expected));
}
```

---

## 🎓 Key NUnit Concepts Covered

### Test Attributes
| Attribute | Purpose |
|-----------|---------|
| `[TestFixture]` | Marks test class |
| `[SetUp]` | Runs before each test |
| `[Test]` | Marks test method |
| `[TestCase(...)]` | Parameterized test data |

### Assertion Types
- **Equality:** `Is.EqualTo()`, `Is.Not.EqualTo()`
- **Comparisons:** `Is.GreaterThan()`, `Is.LessThan()`, `Is.InRange()`
- **Booleans:** `Is.True`, `Is.False`
- **Null:** `Is.Null`, `Is.Not.Null`
- **Strings:** `Does.Contain()`, `Does.StartWith()`, `Does.EndWith()`
- **Collections:** `Has.Count`, `Has.Member()`, `Is.Not.Empty`
- **Exceptions:** `Assert.Throws<T>()`, `Assert.DoesNotThrow()`

---

## ✅ Test Results

When you run `dotnet test`, you should see:

```
Passed!  - Failed:  0, Passed:  8, Skipped:  0

Tests:
  ✓ All_Assertion_Examples
  ✓ Add_MultipleInputs_ReturnsExpected(1, 2, 3)
  ✓ Add_MultipleInputs_ReturnsExpected(-1, 1, 0)
  ✓ Add_MultipleInputs_ReturnsExpected(100, 200, 300)
```

---

## 💡 Learning Objectives

This demo project teaches you:

✅ How to set up NUnit projects
✅ Writing your first unit test
✅ Using test attributes ([TestFixture], [SetUp], [Test])
✅ Writing assertions (Assert.That)
✅ Parameterized testing with [TestCase]
✅ Exception testing
✅ Best practices for organizing tests
✅ Understanding the Arrange-Act-Assert (AAA) pattern

---

## 📚 Additional Documentation

For detailed learning materials, check the NOTES folder or search for:
- **NUnit_Beginner_Guide.md** - Quick start guide
- **NUnit_Complete_Guide.md** - Comprehensive reference

---

## 🛠️ Commands

### Run all tests
```bash
dotnet test
```

### Run with verbose output
```bash
dotnet test --verbosity detailed
```

### Run specific test
```bash
dotnet test --filter "Add_MultipleInputs"
```

### Debug tests
```bash
dotnet test --no-build
```

---

## 📖 File Overview

| File | Type | Purpose |
|------|------|---------|
| `NUnitDemo.slnx` | Solution | Solution file linking projects |
| `NUnitDemo.Core/Class1.cs` | Production | Calculator class with Add/Divide |
| `NUnitDemo.Tests/UnitTest1.cs` | Test | Test cases for Calculator |
| `README.md` | Documentation | This file |

---

## ✨ Features Demonstrated

- ✅ Unit test setup and structure
- ✅ Multiple assertion types
- ✅ Parameterized tests
- ✅ Exception handling in tests
- ✅ Test organization
- ✅ SetUp method for initialization
- ✅ Clean, simple code examples

---

## 🎯 Next Steps

1. **Run the tests** - Execute `dotnet test`
2. **Read the code** - Examine `UnitTest1.cs` and `Class1.cs`
3. **Modify tests** - Change test values and run again
4. **Add new tests** - Write tests for new methods
5. **Explore assertions** - Try different assertion types
6. **Learn mocking** - Next topic (Moq framework)

---

**Status:** ✅ Complete and Ready to Run
**Last Updated:** January 29, 2026

## 📊 Documentation Stats

| Guide | Focus | Coverage |
|-------|-------|----------|
| **Beginner** | Quick learning | All essentials |
| **Complete** | Comprehensive reference | Advanced patterns |
| **Code Examples** | Practical learning | Real implementations |

---

## 🔑 Core Concepts Covered

### Fundamentals
- ✅ What is NUnit
- ✅ Unit testing principles
- ✅ Project architecture
- ✅ Setup instructions

### Practical Skills
- ✅ Writing your first test
- ✅ Using test attributes
- ✅ Writing assertions
- ✅ Parameterized tests
- ✅ Exception testing
- ✅ Debugging tests

### Best Practices
- ✅ AAA pattern (Arrange-Act-Assert)
- ✅ Test naming conventions
- ✅ Test independence
- ✅ Edge case testing
- ✅ Common mistakes to avoid

### Advanced Topics
- ✅ Test organization strategies
- ✅ Test categorization
- ✅ Test lifecycle management
- ✅ Real-world examples

---

## 💡 How to Use These Guides

### Scenario 1: "I'm New to Testing"
1. Open `NUnit_Beginner_Guide.md`
2. Follow the 5-minute setup
3. Look at the "Your First Test" section
4. Run `dotnet test` to see it work
5. Experiment with the examples

### Scenario 2: "I Need to Remember a Concept"
1. Use `NUnit_Complete_Guide.md`
2. Use Ctrl+F to find the topic
3. Review the example code
4. Check the quick reference card

### Scenario 3: "I Want to Understand Real Code"
1. Open `UnitTest1.cs` in the editor
2. Read the comments explaining each part
3. Modify the tests to experiment
4. Run tests to see results

### Scenario 4: "I Need a Complete Reference"
1. Start with `NUnit_Beginner_Guide.md` for foundation
2. Move to `NUnit_Complete_Guide.md` for depth
3. Use code examples for practical understanding
4. Refer back as needed

---

## 🚀 Next Steps

### After Reading These Guides:
1. **Write tests for YOUR code** - Apply what you learned
2. **Use [TestCase]** - Practice parameterized tests
3. **Debug tests** - Learn to step through code
4. **Test edge cases** - Think about unusual scenarios
5. **Learn Mocking** - Next advanced topic (Moq)
6. **Measure coverage** - Use Coverlet
7. **Setup CI/CD** - Automate testing

---

## 📝 Test Writing Checklist

When writing a test, verify:
- [ ] Test class has `[TestFixture]` attribute
- [ ] Test method has `[Test]` attribute
- [ ] Test name follows: `Method_Scenario_Result` pattern
- [ ] AAA pattern is clear (Arrange-Act-Assert)
- [ ] Uses `Assert.That()` with proper constraint
- [ ] Tests one behavior only
- [ ] Independent from other tests
- [ ] Includes edge cases
- [ ] No magic numbers or unclear data
- [ ] Descriptive assertion messages

---

## 🔗 Related Files in Your Project

```
NUnitDemo/
├── NUnit_Beginner_Guide.md         ← START HERE!
├── NUnit_Complete_Guide.md         ← Comprehensive reference
├── NUnitDemo.Core/
│   └── Class1.cs                   ← Production code (Calculator)
├── NUnitDemo.Tests/
│   └── UnitTest1.cs                ← Test examples
└── Other documentation files
```

---

## ✅ Documentation Completeness

- ✅ Setup instructions
- ✅ Basic concepts
- ✅ Test attributes explained
- ✅ Assertions with examples
- ✅ Best practices
- ✅ Common mistakes
- ✅ Debugging techniques
- ✅ Real-world examples
- ✅ Quick reference cards
- ✅ Organization patterns

---

## 📞 Common Questions

**Q: Where should I start?**
A: Start with `NUnit_Beginner_Guide.md`. It's the quickest path to understanding NUnit.

**Q: I forgot what an assertion does?**
A: Check the "Assertions" section in `NUnit_Beginner_Guide.md` - it has a quick table.

**Q: I need a complete reference?**
A: Use `NUnit_Complete_Guide.md`. Includes everything with examples.

**Q: How do I see code examples?**
A: Open `NUnitDemo.Tests/UnitTest1.cs` to see working examples.

**Q: What's the most common mistake?**
A: Testing multiple things in one test. Keep tests focused on one behavior.

---

**Last Updated:** January 29, 2026
**Format:** Markdown (.md files)
**Status:** ✅ Complete and Beginner-Friendly

Good luck with your NUnit learning! 🎯
