
int Divide(int a, int b)
{
    try
    {
        return a / b;
    }
    catch (DivideByZeroException ex)
    {
        throw new ArgumentException("Denominator cannot be zero.", ex);
    }
}


int ReadAndDivide()
{
    Console.Write("Numerator: ");
    int a = int.Parse(Console.ReadLine()!);
    Console.Write("Denominator: ");
    int b = int.Parse(Console.ReadLine()!);
    return Divide(a, b);
}

try
{
    Console.WriteLine(ReadAndDivide());
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);

    if (ex.InnerException != null)
    {
        Console.WriteLine(
            $"Inner: {ex.InnerException.Message}");
    }
}