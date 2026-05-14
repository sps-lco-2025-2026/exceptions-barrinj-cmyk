string[] names = { "Alice", "Bob", "Charlie" };
Console.Write("Enter an index: ");
try
{
int i = int.Parse(Console.ReadLine()!);
Console.WriteLine(names[i]);
}
catch (IndexOutOfRangeException)
{Console.WriteLine("Index is out of range.");
}
catch (FormatException)
{Console.WriteLine("Input is not a valid integer.");
}
