# Between - Comparison Extensions for C#

[![NuGet](https://img.shields.io/nuget/v/Between.svg)](https://www.nuget.org/packages/Between/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight and generic library for C# that provides elegant extension methods to check if a value is between two bounds.

**Syntactic sugar** for range comparisons that makes your code more readable and expressive, inspired by SQL's `BETWEEN` operator.

## ?? Features

- **Syntactic Sugar**: Clean, readable syntax that replaces verbose comparison code
- **Generic**: Works with any type that implements `IComparable<T>`
- **Flexible**: Supports inclusive and exclusive comparisons with granular control
- **Performance**: No overhead, direct compilation - just syntactic sugar!
- **Multi-framework**: Supports .NET Framework 4.6, .NET Standard 2.0, .NET 5, 6, 7, and 8
- **Type-safe**: Fully typed with compile-time validation
- **Well documented**: Complete IntelliSense with XML documentation

## ?? Installation

Install the NuGet package using the .NET CLI:

```bash
dotnet add package Between
```

Or using the Package Manager Console:

```powershell
Install-Package Between
```

## ?? Usage

### Why Use This Library? (Syntactic Sugar Explained)

Instead of writing verbose comparison code:

```csharp
// ? Traditional way - verbose and harder to read
if (age >= 18 && age <= 35)
{
    // ...
}

// ? With Between - clean and expressive syntactic sugar
if (age.Between(18, 35))
{
    // ...
}
```

This library is **syntactic sugar** - it doesn't add functionality, but makes your code more readable and maintainable by expressing intent clearly.

#### More Comparison Examples

| Traditional C# | With Between (Syntactic Sugar) |
|---|---|
| `if (value >= 10 && value <= 20)` | `if (value.Between(10, 20))` |
| `if (value > 10 && value < 20)` | `if (value.Between(10, 20, BetweenBounds.Exclusive))` |
| `if (value > 10 && value <= 20)` | `if (value.Between(10, 20, BetweenBounds.ExcludeLower))` |
| `if (value >= 10 && value < 20)` | `if (value.Between(10, 20, BetweenBounds.ExcludeUpper))` |
| `numbers.Where(n => n >= 10 && n <= 20)` | `numbers.Where(n => n.Between(10, 20))` |

### Basic Usage

```csharp
using Between;

// Integers
int age = 25;
bool isYoungAdult = age.Between(18, 35); // true

// Decimals
double temperature = 23.5;
bool isComfortable = temperature.Between(20.0, 26.0); // true

// Dates
DateTime date = new DateTime(2023, 6, 15);
DateTime start = new DateTime(2023, 1, 1);
DateTime end = new DateTime(2023, 12, 31);
bool thisYear = date.Between(start, end); // true

// Strings (alphabetical comparison)
string name = "Carlos";
bool betweenAandD = name.Between("Alberto", "Daniel"); // true

// Decimals
decimal price = 99.99m;
bool inRange = price.Between(50.0m, 150.0m); // true
```

### Boundary Control with BetweenBounds Enum

The library provides granular control over boundary inclusion through the `BetweenBounds` enum:

```csharp
int value = 10;

// Inclusive (default) - includes both bounds [10, 20]
value.Between(10, 20, BetweenBounds.Inclusive); // true (10 >= 10 && 10 <= 20)

// Exclusive - excludes both bounds (10, 20)
value.Between(10, 20, BetweenBounds.Exclusive); // false (10 > 10 && 10 < 20)

// ExcludeLower - excludes only lower bound (10, 20]
value.Between(10, 20, BetweenBounds.ExcludeLower); // false (10 > 10 && 10 <= 20)

// ExcludeUpper - excludes only upper bound [10, 20)
value.Between(10, 20, BetweenBounds.ExcludeUpper); // true (10 >= 10 && 10 < 20)
```

### Examples with Different Boundary Modes

```csharp
int value = 15;

// All will return true for value = 15
value.Between(10, 20, BetweenBounds.Inclusive);     // true
value.Between(10, 20, BetweenBounds.Exclusive);     // true
value.Between(10, 20, BetweenBounds.ExcludeLower);  // true
value.Between(10, 20, BetweenBounds.ExcludeUpper);  // true

// Edge cases with value = 10
value = 10;
value.Between(10, 20, BetweenBounds.Inclusive);     // true  (includes lower)
value.Between(10, 20, BetweenBounds.Exclusive);     // false (excludes lower)
value.Between(10, 20, BetweenBounds.ExcludeLower);  // false (excludes lower)
value.Between(10, 20, BetweenBounds.ExcludeUpper);  // true  (includes lower)

// Edge cases with value = 20
value = 20;
value.Between(10, 20, BetweenBounds.Inclusive);     // true  (includes upper)
value.Between(10, 20, BetweenBounds.Exclusive);     // false (excludes upper)
value.Between(10, 20, BetweenBounds.ExcludeLower);  // true  (includes upper)
value.Between(10, 20, BetweenBounds.ExcludeUpper);  // false (excludes upper)
```

### Custom Types

Works with any type that implements `IComparable<T>`:

```csharp
public class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person other)
    {
        return Age.CompareTo(other.Age);
    }
}

var person = new Person { Name = "Ana", Age = 30 };
var min = new Person { Name = "Min", Age = 18 };
var max = new Person { Name = "Max", Age = 65 };

bool inWorkingAgeRange = person.Between(min, max); // true
```

##  Use Cases

### Age Validation

```csharp
public bool IsAdult(int age)
{
    return age.Between(18, 120);
}

public bool IsChild(int age)
{
    return age.Between(0, 12, BetweenBounds.ExcludeUpper);
}

public bool IsTeenager(int age)
{
    return age.Between(13, 19, BetweenBounds.Inclusive);
}
```

### Date Validation

```csharp
public bool IsBusinessHours(DateTime time)
{
    var start = new DateTime(time.Year, time.Month, time.Day, 9, 0, 0);
    var end = new DateTime(time.Year, time.Month, time.Day, 18, 0, 0);
    return time.Between(start, end);
}

public bool IsInCurrentYear(DateTime date)
{
    var yearStart = new DateTime(DateTime.Now.Year, 1, 1);
    var yearEnd = new DateTime(DateTime.Now.Year, 12, 31);
    return date.Between(yearStart, yearEnd);
}
```

### Numeric Range Validation

```csharp
public bool IsSafeTemperature(double temperature)
{
    return temperature.Between(-10.0, 40.0);
}

public bool IsValidPrice(decimal price)
{
    return price.Between(0.01m, 999999.99m, BetweenBounds.Inclusive);
}
```

### Collection Filtering

The syntactic sugar shines in LINQ queries:

```csharp
// ? Without Between - harder to read
var inRange = numbers.Where(n => n >= 10 && n <= 20).ToList();

// ? With Between - clear and concise
var numbers = new List<int> { 1, 5, 10, 15, 20, 25, 30 };
var inRange = numbers.Where(n => n.Between(10, 20)).ToList();
// Result: [10, 15, 20]

// Complex example with multiple conditions
var affordablePrices = products
    .Where(p => p.Price.Between(50.0m, 100.0m))
    .ToList();
```

##  API Reference

### BetweenBounds Enum

```csharp
public enum BetweenBounds
{
    Inclusive = 0,      // Both bounds included [min, max]
    Exclusive = 1,      // Both bounds excluded (min, max)
    ExcludeLower = 2,   // Lower bound excluded (min, max]
    ExcludeUpper = 3    // Upper bound excluded [min, max)
}
```



## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


##  Support

If you find a bug or have a suggestion, please [open an issue](https://github.com/yourusername/Between/issues).

---

**Like the project? Give it a star on GitHub!**
