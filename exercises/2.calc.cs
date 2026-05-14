Console.Write("Enter a number: ");
try{
int n = int.Parse(Console.ReadLine()!);
Console.WriteLine(100 / n);
}
catch (DivideByZeroException)
{
    Console.WriteLine("Number cannot be zero.");
}
catch (FormatException)
{
    Console.WriteLine("Input is not a valid integer.");
}
finally
{
    Console.WriteLine("Operation completed.");
}
