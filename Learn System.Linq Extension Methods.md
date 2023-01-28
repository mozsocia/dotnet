
# Learn System.Linq Extension Methods C# 

teach me extensions.cs in dotnet

The `System.Linq` namespace in C# includes the `Enumerable` class, which provides a set of extension methods for working with IEnumerable-based collections. These extension methods can be used to perform common operations on collections, such as filtering, ordering, and grouping, without the need to write explicit loops.

Here's an example of using the `Where` extension method to filter a collection of integers:

```
using System.Linq;

int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);

```

In the above example, the `Where` method is called on the `numbers` array, and a lambda expression is passed as an argument to specify the condition for filtering. The `Where` method returns a new collection containing only the elements that satisfy the condition.

You can chain multiple extension methods together to perform more complex operations. For example, you could use the `Where` method to filter a collection of strings, and then use the `OrderBy` method to sort the resulting collection:

```
string[] names = { "Alice", "Bob", "Charlie", "David" };

IEnumerable<string> sortedNames = names
    .Where(n => n.Length > 4)
    .OrderBy(n => n);

```

In this example, the `Where` method is used to filter the names to only include those with more than 4 characters, and the `OrderBy` method is used to sort the resulting collection alphabetically.

You can also use extension methods from other libraries. For example, you could use the `Select` method from the `System.Linq` namespace to project the elements of a collection into a new form, and then use the `Sum` method from the `MoreLinq` library to calculate the sum of the projected elements:

```
using System.Linq;
using MoreLinq;
int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
int sumOfSquares = numbers
    .Select(n => n * n)
    .Sum();

```

It's worth noting that you can also write your own extension methods.

This is just a brief overview of extension methods in C#, but there's much more you can do with them. I recommend reading the Microsoft documentation on LINQ and extension methods for more information.

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

Linq methods can be applied in which of collections??

The LINQ extension methods provided by the `System.Linq` namespace can be applied to any collection that implements the `IEnumerable<T>` interface. This includes many of the built-in collection classes in C#, such as `List<T>`, `Array`, and `Dictionary<TKey, TValue>`, as well as custom collections that implement the `IEnumerable<T>` interface.

For example, you can use LINQ methods on `List<T>`, `IEnumerable<T>`, `Array`, `Dictionary<TKey, TValue>`, `Queue<T>`, `Stack<T>`, `ObservableCollection<T>` and many more.

In addition, there are also specialized collections that implement other interfaces, such as `IList<T>` and `ICollection<T>`, which provide additional methods for working with the collection. These collections can also be used with LINQ, although the available methods may be more limited than with an `IEnumerable<T>`\-based collection.

It's also worth noting that LINQ to Objects can also be used to query in-memory data structures such as arrays and lists. LINQ also have special implementations for querying SQL databases (LINQ to SQL) and XML documents (LINQ to XML)

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

give me list of all IEnumerable<T>-based collection

Here is a list of some of the built-in collection classes in C# that implement the `IEnumerable<T>` interface:

-   `List<T>`: A dynamic array that allows you to add, remove, and access elements by index.
-   `Array`: A fixed-size collection of elements of the same type.
-   `Dictionary<TKey, TValue>`: A collection of key-value pairs, where each key is unique.
-   `Queue<T>`: A collection that implements a first-in, first-out (FIFO) queue.
-   `Stack<T>`: A collection that implements a last-in, first-out (LIFO) stack.
-   `LinkedList<T>`: A doubly-linked list, which can be useful for efficient insertions and deletions.
-   `HashSet<T>`: A collection that stores unique elements and provides fast lookups.
-   `SortedSet<T>`: A collection that stores unique elements and maintains their order.
-   `ConcurrentBag<T>`: A thread-safe collection that allows multiple threads to add and remove items concurrently.
-   `ConcurrentQueue<T>`: A thread-safe collection that implements a FIFO queue.
-   `ConcurrentStack<T>`: A thread-safe collection that implements a LIFO stack.

It's worth noting that these are just a few examples, and there are many more collection classes that implement `IEnumerable<T>` interface. Some of them are specialized collections that implement additional interfaces, such as `IList<T>` and `ICollection<T>`
