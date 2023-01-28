
give full code with example for extensions method in dotnet core console app

Here is an example of an extension method in a .NET Core console application:

```
using System;

namespace ExtensionMethodsExample
{
    public static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = "Hello, World!";
            string reversed = input.Reverse();
            Console.WriteLine(reversed);
        }
    }
}

```

In this example, the `Reverse` method is defined as an extension method for the `string` type. The `this` keyword before the first parameter indicates that this method is an extension method and the first parameter is the object that the method is being added to.

The `Main` method in the `Program` class uses the `Reverse` method as if it were a built-in method of the `string` type.

When you run this program, it will output the following:

```
!dlroW ,olleH

```

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

give me full code for a int

Here is an example of an extension method for the `int` type in a .NET Core console application:

```
using System;

namespace ExtensionMethodsExample
{
    public static class IntExtensions
    {
        public static int Double(this int input)
        {
            return input * 2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int input = 5;
            int doubled = input.Double();
            Console.WriteLine(doubled);
        }
    }
}

```

In this example, the `Double` method is defined as an extension method for the `int` type. The `this` keyword before the first parameter indicates that this method is an extension method and the first parameter is the object that the method is being added to.

The `Main` method in the `Program` class uses the `Double` method as if it were a built-in method of the `int` type.

When you run this program, it will output the following:

```
10

```

You can also add more extension methods for int as per your requirement.
