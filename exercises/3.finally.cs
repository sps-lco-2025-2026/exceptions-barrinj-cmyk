// exercise 3

Console.Write("Enter a number: ");
try
{
    int n = int.Parse(Console.ReadLine()!);
    if (n%2 == 0)
    {
        Console.WriteLine("Even");
    }
    else
    {
        Console.WriteLine("Odd");
    }
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
    Console.WriteLine("Thank you for using the program.");
}