// Snippet A
try{
int[] arr = new int[3];
arr[10] = 5;
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine("IndexOutOfRangeException " + ex.Message);
}

// Snippet B
string s = null!;
try
{
    Console.WriteLine(s.Length);
}
catch (NullReferenceException ex)
{
    Console.WriteLine("NullReferenceException: " + ex.Message);
}

// Snippet C
int x = int.MaxValue;
try
{
    checked { x = x + 1; }
}
catch (OverflowException ex)
{
    Console.WriteLine("OverflowException: " + ex.Message);
}
